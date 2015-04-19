using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour {

	public event System.EventHandler CellUpdated;
	public event System.EventHandler OwnerUpdated;

	[SerializeField]
	int xPos;
	public int XPos {
		get { return xPos; }
	}

	[SerializeField]
	int yPos;
	public int YPos {
		get { return yPos; }
	}
	
	[SerializeField]
	int owner;
	public int Owner {
		get { return owner; }
	}
	
	[SerializeField]
	bool alive;
	public bool Alive {
		get { return alive; }
	}
	
	List<Cell> adjacentCells = new List<Cell>();


	bool nextState;
	public bool NextState {
		get { return nextState; }
	}
	int nextOwner = 0;
	public int NextOwner {
		get { return nextOwner; }
	}
	int liveCount = 0;
	Grid grid;

	System.EventArgs blankEvent;

	Dictionary<int, int> adjacentOwnerDict = new Dictionary<int, int>();

	public void Initialize(int newXPos, int newYPos, Grid newGrid){
		xPos = newXPos;
		yPos = newYPos;
		grid = newGrid;
		if(Random.Range(0,2) > 0){
			alive = false;
		}

		blankEvent = new System.EventArgs();
		if(CellUpdated != null){
			CellUpdated(this, blankEvent);
		}
	}

	public void InitializeAdjacentCells(Grid grid){
//		string adjacentList = "";
		adjacentCells.Clear();
		List<Cell> foundCells = grid.GetAdjacentCells(xPos, yPos);
		for(int i = 0; i < foundCells.Count; i++){
			adjacentCells.Add(foundCells[i]);
//			adjacentList += "[" + foundCells[i].xPos + "," + foundCells[i].yPos + "]";
		}
//		Debug.LogWarning("Cell " + xPos + ", " + yPos + " adjacent Cells: " + adjacentList);
	}

	public void Live(){
		// Rules for living here
		liveCount = 0;
		for(int i = 0; i < adjacentCells.Count; i++){
			if(adjacentCells[i].Alive){
				liveCount ++;
				int ownerNumber = 0;
				if(adjacentOwnerDict.TryGetValue(adjacentCells[i].owner, out ownerNumber)){
					adjacentOwnerDict[adjacentCells[i].owner] += 1;
				} else if(adjacentCells[i].owner > 0) {
					adjacentOwnerDict.Add(adjacentCells[i].owner, 1);
				}
			}
		}
//		Any live cell with fewer than two live neighbours dies, as if caused by under-population.
		if(alive && liveCount < 2){
			nextState = false;
			nextOwner = 0;
		}
//		Any live cell with two or three live neighbours lives on to the next generation.
		else if(alive && liveCount == 2 || liveCount == 3){
			nextState = true;
			nextOwner = GetNewOwner();
		}
//		Any live cell with more than three live neighbours dies, as if by overcrowding.
		else if(alive && liveCount > 3){
			nextState = false;
			nextOwner = 0;
		}
//		Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
		else if(!alive && liveCount == 3){
			nextState = true;
			nextOwner = GetNewOwner();
		}

	}

	private int GetNewOwner(){
		int mostOwner = owner;
		List<int> ownerKeys = new List<int>(adjacentOwnerDict.Keys);
		for(int i = 0; i < ownerKeys.Count; i ++){
			if(mostOwner == 0 && ownerKeys[i] != 0){
				mostOwner = ownerKeys[i];
			}
			if(adjacentOwnerDict[ownerKeys[i]] > mostOwner){
				mostOwner = ownerKeys[i];
			} else if (adjacentOwnerDict[ownerKeys[i]] == mostOwner){
				// If we want the player in last to take the new cell
				mostOwner = grid.GetLastPlacePlayer();
				// If we want the player in the lead to take the new cell
//				mostOwner = grid.GetFirstPlacePlayer();
			}
		}
		return mostOwner;
	}

	public void UpdateLive(){
		if(owner != nextOwner){
			owner = nextOwner;
			if(OwnerUpdated != null){
				OwnerUpdated(this, blankEvent);
			}
		}
		if(alive != nextState){
			alive = nextState;
			if(CellUpdated != null){
				CellUpdated(this, blankEvent);
			}
		}
	}

	public bool Revive(int o){
		if(alive){
			return false;
		}
		alive = true;
		owner = nextOwner = o;

		if(CellUpdated != null){
			CellUpdated(this, blankEvent);
		}
		return true;
	}

//	void OnDrawGizmos(){
//		Gizmos.color = alive ? Color.white : Color.black;
//		Gizmos.DrawWireCube(new Vector3(xPos, yPos, 0), new Vector3(1,1,1));
//	}
}

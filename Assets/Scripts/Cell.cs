using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour {

	public event System.EventHandler CellUpdated;

	[SerializeField]
	int xPos;
	public int XPos {
		get { return xPos; }
		set { xPos = value; }
	}

	[SerializeField]
	int yPos;
	public int YPos {
		get { return yPos; }
		set { yPos = value; }
	}
	
	[SerializeField]
	int owner;
	public int Owner {
		get { return owner; }
		set { owner = value; }
	}
	
	[SerializeField]
	bool alive;
	public bool Alive {
		get { return alive; }
	}
	
	List<Cell> adjacentCells = new List<Cell>();

	bool nextState;
	int liveCount = 0;

	System.EventArgs blankEvent;

	public void Initialize(int newXPos, int newYPos){
		xPos = newXPos;
		yPos = newYPos;
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
			}
		}
//		Any live cell with fewer than two live neighbours dies, as if caused by under-population.
		if(alive && liveCount < 2){
			nextState = false;
		}
//		Any live cell with two or three live neighbours lives on to the next generation.
		if(alive && liveCount == 2 || liveCount == 3){
			nextState = true;
		}
//		Any live cell with more than three live neighbours dies, as if by overcrowding.
		if(alive && liveCount > 3){
			nextState = false;
		}
//		Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
		if(!alive && liveCount == 3){
			nextState = true;
		}

	}

	public void UpdateLive(){
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
		owner = o;

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

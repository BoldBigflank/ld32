using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour {

	public event System.EventHandler CellUpdated;

	Grid grid;

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
		set { alive = value; }
	}

	public void Initialize(Grid newGrid, int newXPos, int newYPos){
		grid = newGrid;
		xPos = newXPos;
		yPos = newYPos;
		if(Random.Range(0,2) > 0){
			alive = true;
		}

		if(CellUpdated != null){
			CellUpdated(this, new System.EventArgs());
		}
	}

	public void Live(){
		// Rules for living here
		List<Cell> adjacentCells = grid.GetAdjacentCells(xPos, yPos);
		int liveCount = 0;
		for(int i = 0; i < adjacentCells.Count; i++){
			if(adjacentCells[i].Alive){
				liveCount ++;
			}
		}
//		Any live cell with fewer than two live neighbours dies, as if caused by under-population.
		if(alive && liveCount < 2){
			alive = false;
		}
//		Any live cell with two or three live neighbours lives on to the next generation.
		if(alive && liveCount == 2 || liveCount == 3){
			alive = true;
		}
//		Any live cell with more than three live neighbours dies, as if by overcrowding.
		if(alive && liveCount > 3){
			alive = false;
		}
//		Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
		if(!alive && liveCount == 3){
			alive = true;
		}

		if(CellUpdated != null){
			CellUpdated(this, new System.EventArgs());
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = alive ? Color.white : Color.black;
		Gizmos.DrawWireCube(new Vector3(xPos, yPos, 0), new Vector3(1,1,1));
	}
}

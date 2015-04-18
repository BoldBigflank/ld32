using UnityEngine;
using System.Collections;

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
	}

	public void Live(){
		// Rules for living here
		if(CellUpdated != null){
			CellUpdated(this, new System.EventArgs());
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = alive ? Color.white : Color.black;
		Gizmos.DrawWireCube(new Vector3(xPos, yPos, 0), new Vector3(1,1,1));
	}
}

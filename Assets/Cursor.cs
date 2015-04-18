using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	[SerializeField]
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

	// Use this for initialization
	void Start () {
		
	}
	
	public void Activate(){

		Cell cell = grid.GetCell(xPos, yPos);
		Debug.LogWarning("Hit actiave on cell " + cell);
		if(cell != null){
			cell.Alive = true;
		}
	}

	public void Move(int x, int y){
		if(xPos + x >= 0 && xPos + x < grid.XSize){
			xPos += x;
		}
		if(yPos + y >= 0 && yPos + y < grid.YSize){
			yPos += y;
		}
	}
}

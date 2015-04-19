using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public event System.EventHandler CursorUpdated;

	System.EventArgs blankEvent;

	[SerializeField]
	Grid grid;

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

	void Start(){
		blankEvent = new System.EventArgs();
	}

	public void Activate(){
		Cell cell = grid.GetCell(xPos, yPos);
		if(cell != null){
			cell.Revive();
		}
	}

	public void Move(int x, int y){
		if(xPos + x >= 0 && xPos + x < grid.XSize){
			xPos += x;
		}
		if(yPos + y >= 0 && yPos + y < grid.YSize){
			yPos += y;
		}

		if(CursorUpdated != null){
			CursorUpdated(this, blankEvent);
		}
	}
}

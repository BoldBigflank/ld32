using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public event System.EventHandler CursorUpdated;

	System.EventArgs blankEvent;
	
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

	[SerializeField]
	PlayerState playerState;

	public int PlayerNumber{
		get{return playerState.PlayerNumber;}
	}
	
	void Start(){
		
		grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();

		blankEvent = new System.EventArgs();
		switch(playerState.PlayerNumber){
		case 1:
			xPos = 0;
			yPos = grid.YSize-1;
			break;
		case 2:
			xPos = grid.XSize-1;
			yPos = grid.YSize-1;
			break;
		case 3:
			xPos = 0;
			yPos = 0;
			break;
		case 4:
			xPos = grid.XSize-1;
			yPos = 0;
			break;
		default:
			xPos = Random.Range(0, grid.XSize);
			yPos = Random.Range(0, grid.YSize);
			break;
		}
	}

	public void Activate(){
		Cell cell = grid.GetCell(xPos, yPos);
		if(cell != null){
			if(!cell.Revive(playerState.PlayerNumber)){
				// Wrong input, animate with X

			} else  {
				// It worked, SLAM THOSE CURSORS
			};
		}
	}

	public void Move(int x, int y){
		if(xPos + x >= 0 && xPos + x < grid.XSize){
			xPos += x;
		}
		if(yPos + y >= 0 && yPos + y < grid.YSize){
			yPos += y;
		}

		if(x != 0 || y != 0){
			if(CursorUpdated != null){
				CursorUpdated(this, blankEvent);
			}
		}
	}
}

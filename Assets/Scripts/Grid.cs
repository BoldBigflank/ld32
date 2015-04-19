using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	[SerializeField]
	GameObject cellPrefab;

	[SerializeField]
	int xSize;
	public int XSize {
		get { return xSize; }
	}

	[SerializeField]
	int ySize;
	public int YSize {
		get { return ySize; }
	}

	Cell[] cells;
	List<Cell> adjacentCellContainer = new List<Cell>();
	int arrayPosition = 0;
	int yMaxLimit = 0;
	int yMinLimit = 0;
	int right = 0;
	int left = 0;
	int up = 0;
	int down = 0;
	int rightUp = 0;
	int rightDown = 0;
	int leftUp = 0;
	int leftDown = 0;

	Dictionary<int, int> playerScores = new Dictionary<int, int>();

	void Start(){
		cells = new Cell[xSize * ySize];
		for(int x = 0; x < xSize; x ++){
			for(int y = 0; y < ySize; y ++){
				GameObject cellObject = (GameObject)Instantiate (cellPrefab);
				cellObject.transform.parent = transform;
				cells[ySize*x+y] = cellObject.GetComponent<Cell>();
				cells[ySize*x+y].Initialize(x, y, this);
				CellView cellView = cellObject.GetComponent<CellView>();
				cellView.Initialize(cells[ySize*x+y]);
			}
		}
		for(int i = 0; i < cells.Length; i ++){
			cells[i].InitializeAdjacentCells(this);
		}
	}

	public void UpdateTick(){
		for(int i = 0; i < cells.Length; i++){
			cells[i].Live();
		}
		List<int> keyList = new List<int>(playerScores.Keys);
		for(int i = 0; i < keyList.Count; i++){
			playerScores[keyList[i]] = 0;
		}
		for(int i = 0; i < cells.Length; i++){
			cells[i].UpdateLive();
			if(!cells[i].Alive){
				if(playerScores.ContainsKey(cells[i].Owner)){
					playerScores[cells[i].Owner] ++;
				} else if (cells[i].Owner > 0) {
					playerScores.Add(cells[i].Owner, 1);
				}
			}
		}
	}

	public List<Cell> GetAdjacentCells(int xPos, int yPos){
		adjacentCellContainer.Clear();
		arrayPosition = ySize*xPos+yPos;
		yMaxLimit = (yPos == ySize-1 ? 0 : 1);
		yMinLimit = (yPos == 0 ? 0 : 1);
		right = arrayPosition + ySize;
		if(right < cells.Length && right >= 0){
			adjacentCellContainer.Add(cells[right]);
		}
		left = arrayPosition - ySize;
		if(left < cells.Length && left >= 0){
			adjacentCellContainer.Add(cells[left]);
		}
		if(yMaxLimit != 0){
			up = arrayPosition + yMaxLimit;
			if(up < cells.Length && up >= 0){
				adjacentCellContainer.Add(cells[up]);
			}
			rightUp = arrayPosition + ySize + yMaxLimit;
			if(rightUp < cells.Length && rightUp > 0){
				adjacentCellContainer.Add(cells[rightUp]);
			}
			leftUp = arrayPosition - ySize + yMaxLimit;
			if(leftUp < cells.Length && leftUp > 0){
				adjacentCellContainer.Add(cells[leftUp]);
			}
		}
		if(yMinLimit != 0){
			down = arrayPosition - yMinLimit;
			if(down < cells.Length && down >= 0){
				adjacentCellContainer.Add(cells[down]);
			}
			rightDown = arrayPosition + ySize - yMinLimit;
			if(rightDown < cells.Length && rightDown > 0){
				adjacentCellContainer.Add(cells[rightDown]);
			}
			leftDown = arrayPosition - ySize - yMinLimit;
			if(leftDown < cells.Length && leftDown > 0){
				adjacentCellContainer.Add(cells[leftDown]);
			}
		}
		return adjacentCellContainer;
	}

	public Cell GetCell(int xPos, int yPos){
		arrayPosition = ySize*xPos+yPos;
		if(arrayPosition < cells.Length && arrayPosition >= 0){
			return cells[arrayPosition];
		} else {
			return null;
		}
	}
	
	private int GetCellArrayIndex(int xPos, int yPos){
		return ySize*xPos+yPos;
	}

	public int GetScore(int playerNumber){
		int scoreValue = 0;
		playerScores.TryGetValue(playerNumber, out scoreValue);
		return scoreValue;
	}

	public int GetFirstPlacePlayer(){
		int firstPlaceOwner = 0;
		int highestScore = 0;
		List<int> keyList = new List<int>(playerScores.Keys);
		for(int i = 0; i < keyList.Count; i++){
			if(playerScores[keyList[i]] > highestScore){
				firstPlaceOwner = keyList[i];
				highestScore = playerScores[keyList[i]];
			}
		}
		return firstPlaceOwner;
	}

	public int GetLastPlacePlayer(){
		int lastPlaceOwner = 0;
		int lowestScore = 0;
		List<int> keyList = new List<int>(playerScores.Keys);
		for(int i = 0; i < keyList.Count; i++){
			if(playerScores[keyList[i]] < lowestScore){
				lastPlaceOwner = keyList[i];
				lowestScore = playerScores[keyList[i]];
			}
		}
		return lastPlaceOwner;
	}
}


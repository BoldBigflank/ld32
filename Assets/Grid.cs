﻿using UnityEngine;
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

	void Start(){
		cells = new Cell[xSize * ySize];
		for(int x = 0; x < xSize; x ++){
			for(int y = 0; y < ySize; y ++){
				GameObject cellObject = (GameObject)Instantiate (cellPrefab);
				cellObject.transform.parent = transform;
				cells[ySize*x+y] = cellObject.GetComponent<Cell>();
				CellView cellView = cellObject.GetComponent<CellView>();
				cellView.Initialize(cells[ySize*x+y]);
				cells[ySize*x+y].Initialize(this, x, y);
			}
		}
	}

	public void UpdateTick(){
		for(int i = 0; i < cells.Length; i++){
			cells[i].Live();
		}
	}

	public List<Cell> GetAdjacentCells(int xPos, int yPos){
		List<Cell> adjacentCells = new List<Cell>();
		int right = ySize*xPos+yPos + xSize;
		if(right < cells.Length && right >= 0){
			adjacentCells.Add(cells[right]);
		}
		int left = ySize*xPos+yPos - xSize;
		if(left < cells.Length && left >= 0){
			adjacentCells.Add(cells[left]);
		}
		int up = ySize*xPos+yPos + (yPos == ySize-1 ? 0 : 1);
		if(up < cells.Length && up >= 0){
			adjacentCells.Add(cells[up]);
		}
		int down = ySize*xPos+yPos - (yPos == 0 ? 0 : 1);
		if(down < cells.Length && down >= 0){
			adjacentCells.Add(cells[down]);
		}

		int rightUp = ySize*xPos+yPos + xSize + (yPos == ySize-1 ? 0 : 1);
		if(rightUp < cells.Length && rightUp > 0){
			adjacentCells.Add(cells[rightUp]);
		}
		int leftUp = ySize*xPos+yPos - xSize + (yPos == ySize-1 ? 0 : 1);
		if(leftUp < cells.Length && leftUp > 0){
			adjacentCells.Add(cells[leftUp]);
		}
		int rightDown = ySize*xPos+yPos + xSize - (yPos == 0 ? 0 : 1);
		if(rightDown < cells.Length && rightDown > 0){
			adjacentCells.Add(cells[rightDown]);
		}
		int leftDown = ySize*xPos+yPos - xSize - (yPos == 0 ? 0 : 1);
		if(leftDown < cells.Length && leftDown > 0){
			adjacentCells.Add(cells[leftDown]);
		}
		return adjacentCells;
	}

	public Cell GetCell(int xPos, int yPos){
		Debug.LogWarning((ySize*xPos+yPos) + "/" + cells.Length);
		if(ySize*xPos+yPos < cells.Length && ySize*xPos+yPos >= 0){
			return cells[ySize*xPos+yPos];
		} else {
			return null;
		}
	}
	
	private int GetCellArrayIndex(int xPos, int yPos){
		return ySize*xPos+yPos;
	}
}

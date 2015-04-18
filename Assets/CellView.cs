using UnityEngine;
using System.Collections;

public class CellView : MonoBehaviour {
	[SerializeField]
	Cell cellScript;
	Color[] ownerColors = new Color[] { Color.white, Color.red, Color.blue } ;

	public void Initialize(Cell newCell){
		cellScript = newCell;
		cellScript.CellUpdated += HandleCellUpdated;
	}

	void HandleCellUpdated (object sender, System.EventArgs e)
	{
		// Run this when updated
		gameObject.transform.position = new Vector3(cellScript.XPos, cellScript.YPos, 0.0F);

		Color cellColor = ownerColors[cellScript.Owner];
		if(!cellScript.Alive) { cellColor = cellColor / 2; }

		gameObject.GetComponent<Renderer>().material.SetColor ("_Color", cellColor);
	}
	
	// Update is called once per frame
	void Update () {

	}




}

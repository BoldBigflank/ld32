using UnityEngine;
using System.Collections;

public class CellViewScript : MonoBehaviour {
	[SerializeField]
	Cell cellScript;
	Color[] ownerColors = new Color[] { Color.red, Color.blue, Color.green } ;
	// XPos, YPos
	// owner
	// alive

	// Use this for initialization
	void Start () {
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

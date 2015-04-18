using UnityEngine;
using System.Collections;

public class CellView : MonoBehaviour {
	[SerializeField]
	Cell cellScript;

	[SerializeField]
	Animation cellAnimations;

	Color[] ownerColors = new Color[] { Color.white, Color.red, Color.blue } ;

	Renderer thisRenderer;

	public void Initialize(Cell newCell){
		cellScript = newCell;
		cellScript.CellUpdated += HandleCellUpdated;

		gameObject.transform.position = new Vector3(cellScript.XPos, cellScript.YPos, 0.0F);
		thisRenderer = gameObject.GetComponent<Renderer>();

		HandleCellUpdated(this, new System.EventArgs());
	}

	void HandleCellUpdated (object sender, System.EventArgs e)
	{
		Color cellColor = ownerColors[cellScript.Owner];
		if(!cellScript.Alive) { cellColor = cellColor / 2; }

		thisRenderer.material.SetColor("_Color", cellColor);

		cellAnimations.Stop();
		cellAnimations.Play("CellClick");
	}
}

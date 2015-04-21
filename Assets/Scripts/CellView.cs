using UnityEngine;
using System.Collections;

public class CellView : MonoBehaviour {
	[SerializeField]
	Cell cellScript;

	Animator cellAnimator;

	string[] owners = new string[] {"empty", "dinohulk", "tekkikat", "mistermango", "fred"};
	enum states { Awake, Sleepy, Sleeping, Waking, Nightmare } ;
	
	Color[] ownerColors = new Color[] { Color.white, Color.red, Color.blue } ;

	Renderer thisRenderer;

	public void Initialize(Cell newCell){
		cellAnimator = gameObject.GetComponent<Animator>();

		cellScript = newCell;
		cellScript.CellUpdated += HandleCellUpdated;

		gameObject.transform.position = new Vector3(cellScript.XPos, cellScript.YPos, 0.0F);
		thisRenderer = gameObject.GetComponent<Renderer>();

		HandleCellUpdated(this, new System.EventArgs());
	}

	void OnDestroy(){
		cellScript.CellUpdated -= HandleCellUpdated;
	}

	void HandleCellUpdated (object sender, System.EventArgs e)
	{
		string cellState = "";
		// Find the state of the cell
		if(cellScript.Alive && cellScript.NextState && (cellScript.NextOwner == cellScript.Owner)) {
			cellState = states.Awake.ToString ();
		}
		if(cellScript.Alive && cellScript.NextState == false) {
			cellState = states.Sleepy.ToString ();
		}
		if(!cellScript.Alive && !cellScript.NextState){
			cellState = states.Sleeping.ToString();
		}
		if(!cellScript.Alive && cellScript.NextState && (cellScript.NextOwner == cellScript.Owner)){
			cellState = states.Waking.ToString();
		}
		if(!cellScript.Alive && cellScript.NextState && (cellScript.NextOwner != cellScript.Owner)){
			cellState = states.Nightmare.ToString();
		}

		thisRenderer.material = MaterialManager.Instance.GetCellMaterial(owners[cellScript.Owner], cellState);
		Color cellColor = ownerColors[cellScript.Owner];
		if(!cellScript.Alive) { cellColor = cellColor / 2; }

//		thisRenderer.material.SetColor("_Color", cellColor);


		cellAnimator.SetTrigger(cellState);
//		cellAnimations.Stop();
//		cellAnimations.Play("CellClick");
	}
}

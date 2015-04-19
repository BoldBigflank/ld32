using UnityEngine;
using System.Collections;

public class CursorView : MonoBehaviour {
	[SerializeField]
	Cursor cursorScript;

	[SerializeField]
	GameObject cursorCorner;

	Color[] ownerColors = new Color[] { Color.green, Color.red, Color.blue } ;

	Renderer[] childRenderers;

	// Use this for initialization
	void Start () {
//		thisRenderer = gameObject.GetComponent<Renderer>();
		childRenderers = gameObject.GetComponentsInChildren<Renderer>();
		Debug.Log ("Found" + childRenderers.Length + " renderers");
		cursorScript.CursorUpdated += HandleCursorUpdated;
		HandleCursorUpdated(this, new System.EventArgs());
	}

	void OnDestroy(){
		cursorScript.CursorUpdated -= HandleCursorUpdated;
	}

	void HandleCursorUpdated (object sender, System.EventArgs e)
	{
		Color cellColor = ownerColors[cursorScript.PlayerNumber];

		foreach(Renderer r in childRenderers){
			Debug.Log ("Editing color" + cellColor);
			r.material.SetColor("_Color", cellColor);
		}

		transform.position = new Vector3(cursorScript.XPos, cursorScript.YPos, 0.0F);
	}

}

using UnityEngine;
using System.Collections;

public class CursorView : MonoBehaviour {
	[SerializeField]
	Cursor cursorScript;

	[SerializeField]
	GameObject cursorCorner;

	Color[] ownerColors = new Color[] { Color.white, Color.green, Color.gray, Color.blue } ;

	Renderer[] childRenderers;

	// Use this for initialization
	void Start () {
		childRenderers = gameObject.GetComponentsInChildren<Renderer>();
		cursorScript.CursorUpdated += HandleCursorUpdated;
		HandleCursorUpdated(this, new System.EventArgs());
	}

	void OnDestroy(){
		cursorScript.CursorUpdated -= HandleCursorUpdated;
	}

	void HandleCursorUpdated (object sender, System.EventArgs e)
	{
		Color cellColor = ownerColors[cursorScript.PlayerNumber];

		for(int r = 0; r < childRenderers.Length; r ++){
			childRenderers[r].material.SetColor("_Color", cellColor);
		}

		transform.position = new Vector3(cursorScript.XPos, cursorScript.YPos, 0.0F);
	}
}

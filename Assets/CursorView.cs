using UnityEngine;
using System.Collections;

public class CursorView : MonoBehaviour {
	[SerializeField]
	Cursor cursorScript;

	[SerializeField]
	GameObject cursorCorner;

	// Use this for initialization
	void Start () {
		// Create 8 cursor corners
//		for(int x = 0; x < 4; x++){
//			for(int y = 0; y < 2; y++){
//				GameObject cursorSegment = (GameObject)Instantiate (cursorCorner);
//				cursorSegment.transform.Rotate(90.0F * x, 180.0F * y, 0.0F);
//				cursorSegment.transform.parent = this.transform;
//				cursorSegment.transform.localPosition = Vector3.zero;
//			}
//		}

		cursorScript.CursorUpdated += HandleCursorUpdated;
		HandleCursorUpdated(this, new System.EventArgs());
	}

	void OnDestroy(){
		cursorScript.CursorUpdated -= HandleCursorUpdated;
	}

	void HandleCursorUpdated (object sender, System.EventArgs e)

	{
		transform.position = new Vector3(cursorScript.XPos, cursorScript.YPos, 0.0F);
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}

using UnityEngine;
using System.Collections;
using InControl;

public class PlayerControl : MonoBehaviour {

	[SerializeField]
	Cursor cursor;

	[SerializeField]
	int playerNumber = 0;
	[SerializeField]
	Key keyActivate = Key.E;
	[SerializeField]
	Key keyLeft = Key.A;
	[SerializeField]
	Key keyRight = Key.D;
	[SerializeField]
	Key keyUp = Key.W;
	[SerializeField]
	Key keyDown = Key.S;

	[SerializeField]
	float cursorRepeatDelay = 0.1f;
	float cursorMovedTimer = 0.0f;

	CursorActions actions;

	void Start(){
		actions = new CursorActions();
		actions.Activate.AddDefaultBinding( keyActivate );
		actions.Left.AddDefaultBinding( keyLeft );
		actions.Right.AddDefaultBinding( keyRight );
		actions.Up.AddDefaultBinding( keyUp );
		actions.Down.AddDefaultBinding( keyDown );
			actions.Device = InputManager.ActiveDevice;

	}


	// Update is called once per frame
	void Update () {
		cursorMovedTimer -= Time.deltaTime;
		if(cursorMovedTimer < 0.0f){
			cursorMovedTimer = 0.0f;
		}

		if(actions.Device != null){
			if(cursorMovedTimer == 0){
				if(actions.Right.IsPressed){
					cursor.Move(1,0);
					cursorMovedTimer = cursorRepeatDelay;
				}
				if(actions.Left.IsPressed){
					cursor.Move(-1,0);
					cursorMovedTimer = cursorRepeatDelay;
				}
				if(actions.Up.IsPressed){
					cursor.Move(0,1);
					cursorMovedTimer = cursorRepeatDelay;
				}
				if(actions.Down.IsPressed){
					cursor.Move(0,-1);
					cursorMovedTimer = cursorRepeatDelay;
				}
			}

			if(actions.Activate.WasPressed){
				cursor.Activate();
			}
		}
	}
}

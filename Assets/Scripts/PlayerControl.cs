using UnityEngine;
using System.Collections;
using InControl;

public class PlayerControl : MonoBehaviour {

	[SerializeField]
	Cursor cursor;
	[SerializeField]
	PlayerState state;

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
	Key keySlow = Key.Q;

	[SerializeField]
	float cursorRepeatDelay = 0.1f;
	float cursorMovedTimer = 0.0f;

	CursorActions actions;

	int xMove;
	int yMove;
	bool setLive;
	bool setSlow;

	void Start(){
		actions = new CursorActions();
		actions.Activate.AddDefaultBinding( keyActivate );
		actions.Left.AddDefaultBinding( keyLeft );
		actions.Right.AddDefaultBinding( keyRight );
		actions.Up.AddDefaultBinding( keyUp );
		actions.Down.AddDefaultBinding( keyDown );
		actions.Slow.AddDefaultBinding( keySlow );

		if(InputManager.Devices.Count > state.PlayerNumber){
			actions.Device = InputManager.Devices[state.PlayerNumber-1];
			actions.Activate.AddDefaultBinding( InputControlType.Action1 );
			actions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
			actions.Left.AddDefaultBinding( InputControlType.DPadLeft );
			actions.Right.AddDefaultBinding( InputControlType.LeftStickRight );
			actions.Right.AddDefaultBinding( InputControlType.DPadRight );
			actions.Up.AddDefaultBinding( InputControlType.LeftStickUp );
			actions.Up.AddDefaultBinding( InputControlType.DPadUp );
			actions.Down.AddDefaultBinding( InputControlType.LeftStickDown );
			actions.Down.AddDefaultBinding( InputControlType.DPadDown );
			actions.Slow.AddDefaultBinding( InputControlType.Action2 );
		} else {
			actions.Device = InputManager.ActiveDevice;
		}
	}


	// Update is called once per frame
	public void UpdateAt () {
#region input logic
		cursorMovedTimer -= Time.deltaTime;
		if(cursorMovedTimer < 0.0f){
			cursorMovedTimer = 0.0f;
		}

		if(cursorMovedTimer == 0){
			if(actions.Device != null){
				if(actions.Right.IsPressed){
					xMove = 1;
					cursorMovedTimer = cursorRepeatDelay;
				}
				if(actions.Left.IsPressed){
					xMove = -1;
					cursorMovedTimer = cursorRepeatDelay;
				}
				if(actions.Up.IsPressed){
					yMove = 1;
					cursorMovedTimer = cursorRepeatDelay;
				}
				if(actions.Down.IsPressed){
					yMove = -1;
					cursorMovedTimer = cursorRepeatDelay;
				}
			}
		}

		setLive = actions.Activate.WasPressed;
		setSlow = actions.Slow.IsPressed;

#endregion

#region input action
		cursor.Move(xMove, yMove);
		if(setLive){
			cursor.Activate();
		}
		state.SetSlow(setSlow);
		
		xMove = 0;
		yMove = 0;

#endregion
	}
}

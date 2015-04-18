using UnityEngine;
using System.Collections;
using InControl;

public class CursorActions : PlayerActionSet {

	public PlayerAction Activate;
	public PlayerAction Left;
	public PlayerAction Right;
	public PlayerAction Up;
	public PlayerAction Down;
	public TwoAxisInputControl Move;
	public PlayerAction Slow;

	// Use this for initialization
	public CursorActions () {
		Activate = CreatePlayerAction("Activate");
		Left = CreatePlayerAction("Left");
		Right = CreatePlayerAction("Right");
		Up = CreatePlayerAction("Up");
		Down = CreatePlayerAction("Down");
		Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
		Slow = CreatePlayerAction("Slow");
	}

}

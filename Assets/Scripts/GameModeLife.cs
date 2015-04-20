using UnityEngine;
using System.Collections;

public class GameModeLife : GameManager {

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if(CountDownTimer(ref roundTimer, deltaTime)){
			roundTimer = roundTime;
			grid.UpdateTick();
		}
	}
}

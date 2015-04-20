using UnityEngine;
using System.Collections;

public class GameModeKill : GameManager {

	bool matchComplete;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		roundTimer = roundTime;
		matchTimer = matchTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(!matchComplete){
			if(CountDownTimer(ref roundTimer, deltaTime)){
				roundTimer = roundTime;
				grid.UpdateTick();
				grid.SetScoresFromDyingCells();
				for(int i = 0; i < playerStates.Length; i++){
					playerStates[i].UpdateScore(grid.GetScore(playerStates[i].PlayerNumber));
				}
			}

			UpdatePlayerControls();
		}

		if(CountDownTimer(ref matchTimer, deltaTime)){
			matchComplete = true;
		}
	}
}

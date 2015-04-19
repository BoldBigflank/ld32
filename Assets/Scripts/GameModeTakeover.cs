using UnityEngine;
using System.Collections;

public class GameModeTakeover : GameManager {
	
	float playerSlowCount = 1.0f;

	bool timerEnded;
	
	[SerializeField]
	int generationsPerRound = 30;

	// Use this for initialization
	void Start () {
		base.Start();
		matchTimer = matchTime;
		roundTimer = roundTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(!timerEnded){
			if(CountDownTimer(ref matchTimer, deltaTime / playerSlowCount)){
				timerEnded = true;
			}
			
			UpdatePlayerControls();

			playerSlowCount = GetPlayerSlowCount();

		} else {
			if(generationsPerRound > 0){
				if(CountDownTimer(ref roundTimer, deltaTime)){
					roundTimer = roundTime;
					grid.UpdateTick();
					grid.SetScoresFromOwnedDeadCells();
					generationsPerRound --;
					if(generationsPerRound == 0){
						MatchComplete();
					}
				}
			}
		}
	}

	void GetPlayerScores ()
	{
		for(int i = 0; i < playerStates.Length; i++){
			playerStates[i].UpdateScore(grid.GetScore(playerStates[i].PlayerNumber));
		}
	}

	private void MatchComplete(){
		int winner = grid.GetFirstPlacePlayer();
		SendMatchCompleted(winner);
	}
}

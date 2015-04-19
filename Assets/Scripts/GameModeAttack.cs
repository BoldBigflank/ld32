using UnityEngine;
using System.Collections;

public class GameModeAttack : GameManager {

	bool timerEnded;

	void Start(){
		base.Start();
	}

	void Update(){
		if(!timerEnded){
			if(matchTimer > 0.0f){
				matchTimer -= deltaTime;
				if(matchTimer <= 0.0f){
					TimerEnd();
					matchTimer = 0.0f;
				}
			}

			for(int i = 0; i < playerControls.Length; i ++){
				playerControls[i].UpdateAt();
			}
			for(int i = 0; i < playerStates.Length; i ++){
				playerStates[i].UpdateAt();
			}

		} else {
			roundTimer += deltaTime;
			if(roundTimer > roundTime){
				roundTimer = 0;
				grid.UpdateTick();
				postMatchRounds --;
				if(postMatchRounds == 0){
					MatchComplete();
				}
			}
		}
	}

	
	private void TimerEnd(){
		timerEnded = true;
	}

	private void MatchComplete(){
		Debug.LogWarning("Match Complete!");
	}


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModeAttack : GameManager {

	bool setupPhase = false;
	bool launchPhase = false;
	
	[SerializeField]
	int maxGenerations = 100;

	Dictionary<int, bool> playerLost = new Dictionary<int, bool>();

	protected override void Start(){
		base.Start();
		for(int i = 0; i < playerStates.Length; i++){
			playerLost.Add(playerStates[i].PlayerNumber, false);
			switch(playerStates[i].PlayerNumber){
			case 1:
				grid.SetCell(0, grid.YSize-1, 1, true, true);
				grid.SetCell(1, grid.YSize-1, 1, true, true);
				grid.SetCell(0, grid.YSize-2, 1, true, true);
				grid.SetCell(1, grid.YSize-2, 1, true, true);
				break;
			case 2:
				grid.SetCell(grid.XSize-1, 0, 2, true, true);
				grid.SetCell(grid.XSize-2, 0, 2, true, true);
				grid.SetCell(grid.XSize-1, 1, 2, true, true);
				grid.SetCell(grid.XSize-2, 1, 2, true, true);
				break;
			case 3:
				grid.SetCell(grid.XSize-1, grid.YSize-1, 3, true, true);
				grid.SetCell(grid.XSize-2, grid.YSize-1, 3, true, true);
				grid.SetCell(grid.XSize-1, grid.YSize-2, 3, true, true);
				grid.SetCell(grid.XSize-2, grid.YSize-2, 3, true, true);
				break;
			}
		}
		matchTimer = matchTime;
		roundTimer = roundTime;
		setupPhase = true;
	}

	void Update(){
		if(setupPhase){
			if(CountDownTimer(ref matchTimer, deltaTime)){
				setupPhase = false;
				launchPhase = true;
			}
			UpdatePlayerControls();
		}
		if(launchPhase){
			if(CountDownTimer(ref roundTimer, deltaTime)){
				grid.UpdateTick();

				roundTimer = roundTime;
				maxGenerations --;
				if(maxGenerations == 0){
					launchPhase = false;
					MatchComplete();
				}
			}
		}
	}

	void MatchComplete(){
		if(!grid.GetCell(0, grid.YSize-1).Alive ||
		   !grid.GetCell(1, grid.YSize-1).Alive ||
		   !grid.GetCell(0, grid.YSize-2).Alive ||
		   !grid.GetCell(1, grid.YSize-2).Alive){
			if(playerLost.ContainsKey(1)){
				playerLost[1] = true;
			}
		}
		if(!grid.GetCell(grid.XSize-1, 0).Alive ||
		   !grid.GetCell(grid.XSize-2, 0).Alive ||
		   !grid.GetCell(grid.XSize-1, 1).Alive ||
		   !grid.GetCell(grid.XSize-2, 1).Alive){
			if(playerLost.ContainsKey(2)){
				playerLost[2] = true;
			}
		}
		List<int> keys = new List<int>(playerLost.Keys);
		for(int i = 0; i < keys.Count; i ++){
			if(!playerLost[keys[i]]){
				Debug.LogWarning("Player " + keys[i] + " survived!");
			}
		}
	}


}

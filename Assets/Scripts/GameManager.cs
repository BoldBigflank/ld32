﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	
	[SerializeField]
	Grid grid;

	[SerializeField]
	float roundTime = 1.0f;
	public float RoundTime {
		get { return roundTime; }
	}

	float roundTimer;
	public float RoundTimer {
		get { return roundTimer; }
	}

	[SerializeField]
	float deltaTime = 16.0f;

	float playerSlowCount = 1.0f;

	PlayerState[] playerStates;

	void Awake(){
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		playerStates = new PlayerState[players.Length];
		for(int i = 0; i < players.Length; i ++){
			playerStates[i] = players[i].GetComponent<PlayerState>();
		}
	}
	
	// Update is called once per frame
	void Update() {

		roundTimer += deltaTime / playerSlowCount;
		if(roundTimer > roundTime){
			roundTimer = 0;
			grid.UpdateTick();
		}

		playerSlowCount = 1.0f;
		for(int i = 0; i < playerStates.Length; i++){
			if(playerStates[i].IsSlowing){
				playerSlowCount ++;
			}
			playerStates[i].UpdateScore(grid.GetScore(playerStates[i].PlayerNumber));
		}
	}
}

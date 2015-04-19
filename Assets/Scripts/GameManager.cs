using UnityEngine;
using System.Collections;


public class MatchWinArgs : System.EventArgs {
	public int winner;

	public MatchWinArgs(int newWinner) {
		winner = newWinner;
	}
}

public class GameManager : MonoBehaviour {

	public event System.EventHandler MatchCompleted;
	
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

	PlayerControl[] playerControls;

	[SerializeField]
	float matchTime = 10.0f;
	public float MatchTime {
		get { return matchTime; }
	}
	float matchTimer;
	public float MatchTimer {
		get { return matchTimer; }
	}
	bool matchComplete = false;

	void Awake(){
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		playerStates = new PlayerState[players.Length];
		playerControls = new PlayerControl[players.Length];
		for(int i = 0; i < players.Length; i ++){
			playerStates[i] = players[i].GetComponent<PlayerState>();
			playerControls[i] = players[i].GetComponent<PlayerControl>();
		}
		matchTimer = matchTime;
	}
	
	// Update is called once per frame
	void Update() {

		if(matchTimer > 0.0f){
			matchTimer -= deltaTime;
			if(matchTimer <= 0.0f){
				MatchEnd();
				matchTimer = 0.0f;
			}
		}

		if(!matchComplete){
			for(int i = 0; i < playerControls.Length; i ++){
				playerControls[i].UpdateAt();
			}
			for(int i = 0; i < playerStates.Length; i ++){
				playerStates[i].UpdateAt();
			}

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

	private void MatchEnd(){
		matchComplete = true;
		int winner = grid.GetFirstPlacePlayer();
		if(MatchCompleted != null){
			MatchCompleted(this, new MatchWinArgs(winner));
		}

	}
}

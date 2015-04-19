using UnityEngine;
using System.Collections;

public class MatchWinArgs : System.EventArgs {
	public int winner;
	public MatchWinArgs(int newWinner){
		winner = newWinner;
	}
}

public class GameManager : MonoBehaviour {

	public event System.EventHandler MatchCompleted;
	
	[SerializeField]
	protected Grid grid;

	[SerializeField]
	protected float roundTime = 1.0f;
	public float RoundTime {
		get { return roundTime; }
	}

	protected float roundTimer;
	public float RoundTimer {
		get { return roundTimer; }
	}

	[SerializeField]
	protected float deltaTime = 16.0f;

	float playerSlowCount = 1.0f;

	protected PlayerState[] playerStates;
	protected PlayerControl[] playerControls;

	[SerializeField]
	protected float matchTime = 10.0f;
	public float MatchTime {
		get { return matchTime; }
	}
	protected float matchTimer;
	public float MatchTimer {
		get { return matchTimer; }
	}
	bool matchComplete = false;

	void Awake(){
		Application.targetFrameRate = 60;
	}

	[SerializeField]
	bool updatePerRound = false;

	[SerializeField]
	protected int postMatchRounds = 100;

	// Use this for initialization
	protected void Start () {
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

			if(updatePerRound){
				roundTimer += deltaTime / playerSlowCount;
				if(roundTimer > roundTime){
					roundTimer = 0;
					grid.UpdateTick();
				}
			}

			playerSlowCount = 1.0f;
			for(int i = 0; i < playerStates.Length; i++){
				if(playerStates[i].IsSlowing){
					playerSlowCount ++;
				}
				playerStates[i].UpdateScore(grid.GetScore(playerStates[i].PlayerNumber));
			}
		}

		if(matchComplete){
			if(postMatchRounds > 0){
				roundTimer += deltaTime / playerSlowCount;
				if(roundTimer > roundTime){
					roundTimer = 0;
					grid.UpdateTick();
					postMatchRounds --;
					if(postMatchRounds == 0){
						MatchEnd();
					}
				}
			}
		}

		for(int i = 0; i < playerStates.Length; i++){
			playerStates[i].UpdateScore(grid.GetScore(playerStates[i].PlayerNumber));
		}
	}

	private void MatchEnd(){
		matchComplete = true;
		if(postMatchRounds == 0){
			postMatchRounds = 3;
			matchComplete = false;
			matchTimer = matchTime;
//			int winner = grid.GetFirstPlacePlayer();
//			if(MatchCompleted != null){
//				MatchCompleted(this, new MatchWinArgs(winner));
//			}
		}

	}
}

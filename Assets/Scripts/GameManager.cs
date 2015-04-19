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
	public event System.EventHandler MatchTimerCompleted;

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

	void Awake(){
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	protected virtual void Start () {
		grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		playerStates = new PlayerState[players.Length];
		playerControls = new PlayerControl[players.Length];
		for(int i = 0; i < players.Length; i ++){
			playerStates[i] = players[i].GetComponent<PlayerState>();
			playerControls[i] = players[i].GetComponent<PlayerControl>();
		}
	}

	protected bool CountDownTimer(ref float currentTimer, float newDeltaTime){
		if(currentTimer > 0.0f){
			currentTimer -= newDeltaTime;
			if(currentTimer <= 0.0f){
				currentTimer = 0.0f;
				return true;
			}
		}
		return false;
	}

	protected void UpdatePlayerControls(){
		for(int i = 0; i < playerControls.Length; i ++){
			playerControls[i].UpdateAt();
		}
		for(int i = 0; i < playerStates.Length; i ++){
			playerStates[i].UpdateAt();
		}
	}

	protected float GetPlayerSlowCount(){
		float slowCount = 1.0f;
		for(int i = 0; i < playerStates.Length; i++){
			if(playerStates[i].IsSlowing){
				slowCount ++;
			}
		}
		return slowCount;
	}

	protected void SendMatchCompleted(int winner){
		if(MatchCompleted != null){
			MatchCompleted(this, new MatchWinArgs(winner));
		}
	}
}

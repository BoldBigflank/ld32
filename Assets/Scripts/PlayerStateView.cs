using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStateView : MonoBehaviour {

	PlayerState state;

	[SerializeField]
	int playerNumber;

	[SerializeField]
	Image playerIcon;

	[SerializeField]
	Text playerName;

	[SerializeField]
	Text playerScore;

	[SerializeField]
	Image slowdownBar;

	int currentScore = 0;

	// Use this for initialization
	void Start () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i ++){
			PlayerState foundState = players[i].GetComponent<PlayerState>();
			if(foundState.PlayerNumber == playerNumber){
				state = foundState;
			}
		}
		if(state == null){
			gameObject.SetActive(false);
		} else {
			state.ScoreChanged += HandleScoreChanged;
		}
	}

	void OnDestroy() {
		if(state != null){
			state.ScoreChanged -= HandleScoreChanged;
		}
	}

	void HandleScoreChanged (object sender, System.EventArgs e)
	{
		if(state.TerritoryScore > currentScore){
			// score up animation
			currentScore = state.TerritoryScore;
		} else if (state.TerritoryScore < currentScore){
			// score down animation
			currentScore = state.TerritoryScore;
		}
		playerScore.text = currentScore.ToString("F0");
	}
	
	// Update is called once per frame
	void Update () {
		slowdownBar.transform.localScale = new Vector3(state.BuildPoints / state.BuildPointMax,
		                                               slowdownBar.transform.localScale.y,
		                                               slowdownBar.transform.localScale.z);
	}
}

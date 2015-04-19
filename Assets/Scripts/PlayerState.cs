using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {

	public event System.EventHandler ScoreChanged;
	System.EventArgs blankArgs;

	[SerializeField]
	int playerNumber;
	public int PlayerNumber {
		get { return playerNumber; }
	}

	int territoryScore;
	public int TerritoryScore {
		get { return territoryScore; }
	}

	[SerializeField]
	float slowTimeMax = 10.0f;
	public float SlowTimeMax {
		get { return slowTimeMax; }
	}
	float slowTimeLeft = 10.0f;
	public float SlowTimeLeft {
		get { return slowTimeLeft; }
	}
	
	[SerializeField]
	bool isSlowing;
	public bool IsSlowing {
		get { return isSlowing; }
	}

	void Start(){
		blankArgs = new System.EventArgs();
		slowTimeLeft = slowTimeMax;
	}

	// Update is called once per frame
	public void UpdateAt () {
		// Limit use of the slow time
		if(isSlowing){
			slowTimeLeft -= Time.deltaTime;
		} else if (slowTimeLeft < slowTimeMax) {
			slowTimeLeft += Time.deltaTime;
			if(slowTimeLeft > slowTimeMax){
				slowTimeLeft = slowTimeMax;
			}
		}
	}

	public void SetSlow(bool holdingSlow){
		if(slowTimeLeft > 0.0f){
			isSlowing = holdingSlow;
		} else {
			isSlowing = false;
		}
	}

	public void UpdateScore(int newScore){
		territoryScore = newScore;
		if(ScoreChanged != null){
			ScoreChanged(this, blankArgs);
		}
	}
}

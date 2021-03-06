﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultUIView : MonoBehaviour {

	GameManager manager;

	[SerializeField]
	Animation panelAnimations;

	[SerializeField]
	Text matchWinner;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		manager.MatchCompleted += HandleMatchCompleted;
	}

	void OnDestroy(){
		manager.MatchCompleted -= HandleMatchCompleted;
	}

	void HandleMatchCompleted (object sender, System.EventArgs e)
	{
		MatchWinArgs matchWinArgs = (MatchWinArgs)e;
		matchWinner.text = "Player " + matchWinArgs.winner + " Wins!";

		panelAnimations.Stop();
		panelAnimations.Play("ShowResultPanel");
	}
}

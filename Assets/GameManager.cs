using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	[SerializeField]
	float roundTime = 1.0f;
	float roundTimer;

	[SerializeField]
	Grid grid;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		roundTimer += Time.deltaTime;
		if(roundTimer > roundTime){
			roundTimer = 0;
			grid.UpdateTick();
		}
	}
}

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	[SerializeField]
	float roundTime = 1.0f;
	float roundTimer;

	[SerializeField]
	Grid grid;

	[SerializeField]
	float deltaTime = 16.0f;

	void Awake(){
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		roundTimer += deltaTime;
		if(roundTimer > roundTime){
			roundTimer = 0;
			grid.UpdateTick();
		}
	}
}

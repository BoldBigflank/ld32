using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIView : MonoBehaviour {

	[SerializeField]
	GameManager manager;

	[SerializeField]
	Image timerBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timerBar.rectTransform.localScale = new Vector3(manager.RoundTimer / manager.RoundTime,
		                                                timerBar.rectTransform.localScale.y,
		                                                timerBar.rectTransform.localScale.z);
	}
}

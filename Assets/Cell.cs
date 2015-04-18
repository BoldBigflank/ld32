using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
	public event System.EventHandler CellUpdated;

	private int xPos;
	public int XPos{
		get{ return xPos; }
		set{ xPos = value; }
	}

	private int yPos;
	public int YPos{
		get{ return yPos; }
		set{ yPos = value; }
	}

	private int owner;
	public int Owner {
		get{ return owner; }
		set{ owner = value; }
	}

	private bool alive;
	public bool Alive{
		get{ return alive; }
		set{ alive = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

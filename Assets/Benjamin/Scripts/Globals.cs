using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class lets us use the static variables wherever we are
/// Use these variables instead of dragging/GetComponenting them
/// </summary>
public class Globals : MonoBehaviour {

	// Player camera control system (WIP)
	// Functions that will control the camera movement
	// ALL camera movement (on the player) code MUST be in here

	// Input system abstraction layer
	// Basically use this to determine button/trigger/joystick changes
	protected static InputSystem input;
	public static InputSystem Input {
		get
		{
			if(!input)
			{
				input = Global.gameObject.AddComponent<InputSystem>();
			}
			return input;
		}
		protected set { input = value; }
	}

	// UI Control System (WIP)
	// A centralised way to control the various UI canvas/elements we will have
	// eg. Open, close and exchange infomation between them

	// Particle spawn Control System (WIP)
	// Deciding whether to put this here
	// This should do all the particle/projectile spawning stuff for 
	// organization purposes

	// 
	// 
	
	// Centralised way to get the player
	public static GameObject player;

	// The tag we use to search for the player (DON'T CHANGE UNLESS NECESSARY)
	public string playerTag = "Player";

	// We keep a reference so we can delete other copies should there be more
	// than one
	protected static Globals globalVar;
	public static Globals Global {
		get
		{
			if(!globalVar)
			{
				GameObject go = new GameObject("Globals");
				globalVar = go.AddComponent<Globals>();
			}
			return globalVar;
		}
		protected set { globalVar = value; }
	}

	// Deciding on whether to put this on Start or Awake cos its meant the be 
	// the very first thing that is meant to be initialized
	public void Start()
	{
		if(Input == null) { }

		player = GameObject.FindGameObjectWithTag(playerTag);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class lets us use the static variables wherever we are
/// Use these variables instead of dragging/GetComponenting them
/// </summary>
[RequireComponent(
	typeof(PlayerCameraController), 
	typeof(InputSystem))
]
public class Globals : MonoBehaviour {

	// Player camera control system (WIP)
	// Functions that will control the camera movement
	// ALL camera movement (on the player) code MUST be in here
	protected static PlayerCameraController cameraController;
	public static PlayerCameraController CameraController
	{
		get
		{
			if(!cameraController)
			{
				cameraController = Global.gameObject.
					GetComponent<PlayerCameraController>();
			}
			return cameraController;
		}
		protected set { cameraController = value; }
	}

	// Input system abstraction layer
	// Basically use this to determine button/trigger/joystick changes
	protected static InputSystem input;
	public static InputSystem Input {
		get
		{
			if(!input)
			{
				input = Global.gameObject.GetComponent<InputSystem>();
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

	// The Player's Camera
	protected static Camera playerCamera;
	public static Camera PlayerCamera
	{
		get
		{
			if(!playerCamera) playerCamera = FindObjectOfType<Camera>();
			return playerCamera;
		}
		protected set { playerCamera = value; }
	}

	// Centralised way to get the player
	static GameObject playerGameobject;
	public static GameObject PlayerGameobject
	{
		get
		{
			if(!playerGameobject)
			{
				playerGameobject = GameObject.FindGameObjectWithTag(playerTag);
			}
			return playerGameobject;
		}
		protected set { playerGameobject = value; }
	}

	// The tag we use to search for the player (DON'T CHANGE UNLESS NECESSARY)
	public const string playerTag = "Player";

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
		if (globalVar == null) globalVar = this;
		else if (globalVar != this) { Destroy(this); return; }

		if(Input == null) { }
		if(CameraController == null) { }
	}
}
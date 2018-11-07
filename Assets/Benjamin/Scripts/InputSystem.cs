using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class KeyBinding 
{
	// Current Controls
	[SerializeField] public static string CharacterMovementX;
	[SerializeField] public static string CharacterMovementY;

	[SerializeField] public static string CameraMovementX;
	[SerializeField] public static string CameraMovementY;

	[SerializeField] public static string DPadHorizontal;
	[SerializeField] public static string DPadVertical;

	[SerializeField] public static string Trigger;
	[SerializeField] public static KeyCode LShoulderButton;
	[SerializeField] public static KeyCode RShoulderButton;

	[SerializeField] public static KeyCode AButton;
	[SerializeField] public static KeyCode BButton;
	[SerializeField] public static KeyCode XButton;
	[SerializeField] public static KeyCode YButton;
}

[System.Serializable]
public class KeyHistory
{
	[SerializeField] public Vector3 movementVector;
	[SerializeField] public Vector2 cameraVector;
	[SerializeField] public Vector2 dPadVector;
	[SerializeField] public float triggerValue;
	[SerializeField] public bool aButtonDown;
	[SerializeField] public bool bButtonDown;
	[SerializeField] public bool xButtonDown;
	[SerializeField] public bool yButtonDown;
	[SerializeField] public bool lShoulderButtonDown;
	[SerializeField] public bool rShoulderButtonDown;
}

public class InputSystem : MonoBehaviour {

	// Mouse Controls
	private const string KeyboardXAxis = "Keyboard_Axis_X";
	private const string KeyboardYAxis = "Keyboard_Axis_Y";

	private const string MouseXAxis = "Mouse_Axis_X";
	private const string MouseYAxis = "Mouse_Axis_Y";
	
	// Controller Controls
	private const string LStickXAxis = "Axis_0";
	private const string LStickYAxis = "Axis_1";

	private const string TriggerAxis = "Axis_2";

	private const string RStickXAxis = "Axis_3";
	private const string RStickYAxis = "Axis_4";

	private const string DPadXAxis = "Axis_5";
	private const string DPadYAxis = "Axis_6";

	private const int AButton = 0;
	private const int BButton = 1;
	private const int XButton = 2;
	private const int YButton = 3;

	private const int LShoulder = 4;
	private const int RShoulder = 5;

	[Range(0, 15)] public int currentJoystick = 0;
	
	public KeyCode switchInput = KeyCode.Tab;

	public bool UseController = true;

	// Save the last X key updates
	public KeyHistory/*[]*/ keyHistory;
	//private int keyHistoryIndex;

	// These values are so we can map the correct button for each joystick
	private const int JoystickButtonIndex0 = (int)KeyCode.Joystick1Button0;
	private const int JoystickButtonMax = 20;

	void Start()
	{
		//keyHistoryIndex = 0;
		keyHistory = new KeyHistory();
		SetupControls();
	}

	public void SetupControls()
	{
		if (UseController) SetupController();
		else SetupMouseKeyboard();
	}

	void SetupController()
	{
		string currentJoy = "Joy_" + currentJoystick + "_";
		KeyCode joystickButtonOffset = (KeyCode)JoystickButtonIndex0 + 
			(JoystickButtonMax * currentJoystick);

		KeyBinding.CharacterMovementX = currentJoy + LStickXAxis;
		KeyBinding.CharacterMovementY = currentJoy + LStickYAxis;

		KeyBinding.CameraMovementX = currentJoy + RStickXAxis;
		KeyBinding.CameraMovementY = currentJoy + RStickYAxis;

		KeyBinding.DPadHorizontal = currentJoy + DPadXAxis;
		KeyBinding.DPadVertical = currentJoy + DPadYAxis;

		KeyBinding.Trigger = currentJoy + TriggerAxis;
		KeyBinding.AButton = joystickButtonOffset + AButton;
		KeyBinding.BButton = joystickButtonOffset + BButton;
		KeyBinding.XButton = joystickButtonOffset + XButton;
		KeyBinding.YButton = joystickButtonOffset + YButton;

		KeyBinding.LShoulderButton = joystickButtonOffset + LShoulder;
		KeyBinding.RShoulderButton = joystickButtonOffset + RShoulder;
	}

	void SetupMouseKeyboard()
	{
		KeyBinding.CharacterMovementX = KeyboardXAxis;
		KeyBinding.CharacterMovementY = KeyboardYAxis;

		KeyBinding.CameraMovementX = MouseXAxis;
		KeyBinding.CameraMovementY = MouseYAxis;
	}

	void UpdateControllerKeys()
	{
		KeyHistory k = keyHistory/*[keyHistoryIndex]*/;

		k.movementVector.y = Input.GetAxis(KeyBinding.CharacterMovementX);
		k.movementVector.z = Input.GetAxis(KeyBinding.CharacterMovementY);

		k.cameraVector.x = Input.GetAxis(KeyBinding.CameraMovementX);
		k.cameraVector.y = Input.GetAxis(KeyBinding.CameraMovementY);

		k.dPadVector.x = Input.GetAxis(KeyBinding.DPadHorizontal);
		k.dPadVector.y = Input.GetAxis(KeyBinding.DPadVertical);

		k.triggerValue = Input.GetAxis(KeyBinding.Trigger);

		k.aButtonDown = Input.GetKey(KeyBinding.AButton);
		k.bButtonDown = Input.GetKey(KeyBinding.BButton);
		k.xButtonDown = Input.GetKey(KeyBinding.XButton);
		k.yButtonDown = Input.GetKey(KeyBinding.YButton);

		k.lShoulderButtonDown = Input.GetKey(KeyBinding.LShoulderButton);
		k.rShoulderButtonDown = Input.GetKey(KeyBinding.RShoulderButton);

		//keyHistoryIndex = (keyHistoryIndex + 1) % keyHistory.Length;
	}

	void Update()
	{
		UpdateControllerKeys();

		if (Input.GetKeyDown(switchInput))
		{
			UseController = !UseController;
			SetupControls();
		}
	}

	public Vector2 CharacterMovement()
	{
		return keyHistory.movementVector;
	}
}

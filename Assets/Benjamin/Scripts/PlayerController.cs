using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	public float rotationSpeed;
	public float movementSpeed;

	private InputSystem inputs;
	private Transform cameraTransform;
	private Transform tf;

	private KeyHistory k;

	void Start () {
		inputs = Globals.Input;
		cameraTransform = FindObjectOfType<Camera>().transform;
		tf = transform;
	}
	
	void Update () {
		k = inputs.keyHistory;
		if(k.movementVector.magnitude != 0)
		{
			tf.Translate(Vector3.forward * movementSpeed);
		}
	}

	void LateUpdate()
	{
		if(k.movementVector.magnitude != 0)
		{
			var currentForward = tf.forward;

			var targetRight = Vector3.Cross(cameraTransform.forward, Vector3.up);
			var targetForward = Vector3.Cross(targetRight, Vector3.up);

			targetForward *= k.movementVector.z;
			targetRight *= k.movementVector.x;

			tf.forward = Vector3.Slerp(currentForward, (targetForward + 
				targetRight).normalized, rotationSpeed * Time.deltaTime);
		}
	}
}
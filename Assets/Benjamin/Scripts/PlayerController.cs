using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {


	public float movementSpeed;
	public Transform cameraTargetPoint;

	private InputSystem inputs;
	private Transform cameraTransform;
	private Transform tf;
	private Rigidbody rb;

	private readonly KeyHistory k;

	void Start () {
		inputs = Globals.Input;
		cameraTransform = Camera.main.transform;
		tf = transform;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		KeyHistory k = inputs.keyHistory;

		if(k.movementVector.magnitude != 0)
		{
			Vector3 forward = tf.forward * k.movementVector.z;
			rb.AddForce(forward * movementSpeed);
			tf.Rotate(0, k.movementVector.y, 0);
			//cameraTransform.Rotate(0, -k.movementVector.y, 0);
			float angle = Vector3.Angle(cameraTransform.position - tf.position,
				cameraTargetPoint.position - tf.position);
			//print(angle);
		}

		if(k.cameraVector.magnitude != 0)
		{
			//tf.Rotate(0, k.cameraVector.x, 0);

			//cameraTransform.LookAt(tf);
			//Vector3 rot = cameraTransform.localEulerAngles;
			//rot.y = 0;
			//cameraTransform.localEulerAngles = rot;
		}
	}
}
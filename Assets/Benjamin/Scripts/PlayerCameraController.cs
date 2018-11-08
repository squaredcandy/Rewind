using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {

	private new Camera camera;
	private Transform cameraTransform;
	private Transform player;

	[Tooltip("The min distance at which we start moving the camera"), Range(1, 10)]
	public float minDistanceThreshold = 5f;

	[Tooltip("The range in which the camera doesn't move"), Range(1, 5)]
	public float distanceThreshold = 2f;

	[Tooltip("The max height that's allow for the camera")]
	public float maxHeight = 5f;

	[Tooltip("The player's offset")]
	public Vector3 pivotOffset;

	[Tooltip("The camera's look offset")]
	public Vector3 lookOffset;

	// The distance from the player
	public float distance;

	public float multiplier;

	void Start () {
		camera = Globals.PlayerCamera;
		cameraTransform = camera.transform;
		player = Globals.PlayerGameobject.transform;
	}
	
	void Update () {


		var positionOffset = player.position + pivotOffset;

		distance = Vector3.Distance(positionOffset, cameraTransform.position);

		float midDistanceThreshold = minDistanceThreshold + 
			(distanceThreshold / 2);

		if(!CameraInSafeZone() || multiplier != 0)
		{
			var direction = cameraTransform.position - positionOffset;
			var desiredPosition = 
				(direction.normalized * midDistanceThreshold) + positionOffset;

			desiredPosition.y = Mathf.Max(Mathf.Min(maxHeight + player.position.y, 
				desiredPosition.y), player.position.y);

			multiplier = distance % midDistanceThreshold;

			if (distance < midDistanceThreshold)
				multiplier = midDistanceThreshold - multiplier;

			cameraTransform.position = Vector3.Lerp(cameraTransform.position, 
				desiredPosition, Time.deltaTime * multiplier);
		}

		Vector3 lookAtDirection = player.position - cameraTransform.position;
		cameraTransform.forward = lookAtDirection + lookOffset;
	}

	bool CameraInSafeZone()
	{
		if (distance < minDistanceThreshold || distance >
			minDistanceThreshold + distanceThreshold) return false;
		return true;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Initialises the distance of the camera from the player
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update the camera's position
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}

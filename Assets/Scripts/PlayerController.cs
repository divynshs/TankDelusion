using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public Rigidbody rb;
	public float speed;
	public float rotation;
	public Boundary boundary;
	public float durationPowerUp;

	public GameObject shot;
	public GameObject superShot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;
	private GameController gameController;
	private bool damage;
	private bool doubleSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController> ();
		if (gameController == null)
			Debug.Log ("Cannot find 'gameController' script");
		damage = false;
	}

	void Update() {
		//MovePlayer ();
		if (Input.GetKeyDown ("space") && Time.time > nextFire) {
			Shoot ();
		}
		StartCoroutine (CheckPower ());
	}

	// Spawns a bullet
	void Shoot() {
		if (Input.GetKeyDown ("space") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			if (damage == false) {
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			} else if (damage == true) { 
				Instantiate (superShot, shotSpawn.position, shotSpawn.rotation);
			}
		}
	}

	IEnumerator CheckPower() {
		if (damage) {
			yield return new WaitForSeconds (durationPowerUp);
			damage = false;
		}
		if (doubleSpeed) {
			yield return new WaitForSeconds (durationPowerUp);
			doubleSpeed = false;
		}
	}
		
	// Moves the player according to the user input
	void FixedUpdate() {
		MovePlayer ();
		Bounds ();
	}

	void MovePlayer() {
		// Move Up
		if (Input.GetKey (KeyCode.W)) {
			if (doubleSpeed == false) {
				rb.velocity = transform.forward * speed;
			} else if (doubleSpeed == true) {
				rb.velocity = transform.forward * speed * 2;
			}
		}

		// Move Down
		if (Input.GetKey (KeyCode.S)) {
			if (doubleSpeed == false) {
				rb.velocity = transform.forward * -speed * 1/2;
			} else if (doubleSpeed == true) {
				rb.velocity = transform.forward * -speed;
			}
		}

		// Move Left
		if (Input.GetKey (KeyCode.A)) {
			if (doubleSpeed == false) {
				transform.Rotate (-Vector3.up * rotation * Time.deltaTime);
			} else if (doubleSpeed == true) {
				transform.Rotate (-Vector3.up * rotation * 2 * Time.deltaTime);
			}
		}

		// Move Right
		if (Input.GetKey (KeyCode.D)) {
		if (doubleSpeed == false) {
			transform.Rotate (Vector3.up * rotation * Time.deltaTime);
		} else if (doubleSpeed == true) {
			transform.Rotate (Vector3.up * rotation * 2 * Time.deltaTime);
			} 
		}
	}

	void Bounds() {
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax));
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Damage") {
			Destroy (other.gameObject);
			damage = true;
		}
		if (other.tag == "Speed") {
			Destroy (other.gameObject);
			doubleSpeed = true;
		}
		if (other.tag == "Life") {
			Destroy(other.gameObject);
			gameController.IncLives ();
		}
		if (other.tag == "Enemy") {
			Destroy (other.gameObject);
			gameController.DeductLives ();
		}
	}
}

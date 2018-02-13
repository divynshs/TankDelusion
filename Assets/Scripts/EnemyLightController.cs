using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightController : MonoBehaviour {

/*	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay; */
	public int score;

	private int hitPoints;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		hitPoints = Random.Range(1,2);
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController> ();
		if (gameController == null)
			Debug.Log ("Cannot find 'gameController' script");
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bullet") {
			Destroy (other.gameObject);
			DecHP ();
		}
		if (other.tag == "SuperBullet") {
			Destroy (other.gameObject);
			DecHP ();
			DecHP ();
			DecHP ();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Wall") {
			Destroy (gameObject);
		}
	}

	void DecHP () {
		hitPoints--;
		if (hitPoints <= 0) {
			Destroy (gameObject);
			gameController.AddScore (score);
		}
	}
}

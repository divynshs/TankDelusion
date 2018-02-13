using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyController : MonoBehaviour {

	private int hitPoints;

	// Use this for initialization
	void Start () {
		hitPoints = 3;
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

	void DecHP () {
		hitPoints--;
		if (hitPoints <= 0)
			Destroy (gameObject);
	}
}

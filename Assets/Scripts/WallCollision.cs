using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Wall")
			Destroy (gameObject);
	}
}

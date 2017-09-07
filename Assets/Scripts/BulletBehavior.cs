using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	private Rigidbody bulletBody;

	// Use this for initialization
	void Start () {
		bulletBody = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		Destroy(this.gameObject, 2.0f);
	}

	void OnCollisionEnter(Collision collision){

		var hit = collision.gameObject;
		var health = hit.GetComponent<PlayerHealth> ();

		if (health != null) {

			health.TakeDamage (10);
		}

		Destroy (gameObject);
	}
}

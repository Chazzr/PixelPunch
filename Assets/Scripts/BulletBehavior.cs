using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelPunch.Weapons;

public class BulletBehavior : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		Destroy(this.gameObject, 10.0f);
	}

	/*void OnCollisionEnter(Collision collision){

		var hit = collision.gameObject;
		var health = hit.GetComponent<PlayerHealth> ();

		if (health != null) {

			health.TakeDamage (dealDamage);
		}

		Destroy (gameObject);
	}*/
}

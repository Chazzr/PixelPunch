using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PixelPunch.Controller;

namespace PixelPunch.Weapons {
	public class WeaponPistol : Weapon {

		public float tracerSpeed = 6.0f;

		private float shotDuration = 0.07f;
		private LineRenderer laserLine;
		private int weaponRange = 10;

		void Start(){
			SetDamage (2);
			laserLine = GetComponent<LineRenderer> ();
		}

		public override void Shoot(){
			
			Debug.Log("Shooting pistol!");

			StartCoroutine (ShotEffect()
			);

			//This will cast a ray against colliders in layer 8 (i.e "Player" layer)
			int layerMask = 1 << 8;  

			RaycastHit hit;

			Vector3 rayOrigin = bulletSpawnPos.position;

			Debug.DrawRay (rayOrigin, bulletSpawnPos.right * weaponRange, Color.green);


			if (Physics.Raycast (rayOrigin, bulletSpawnPos.right, out hit, weaponRange, layerMask)) {
				Debug.Log ("Hit a Player");

				var health = hit.collider.GetComponent<PlayerHealth>();

				if (health != null) {
					health.TakeDamage (damage);
				}

			} else {
				Debug.Log ("Missed a Player");
			}

		}

		public void SetDamage (int d){
			damage = d;
		}

		private IEnumerator ShotEffect(){
			laserLine.enabled = true;

			var tracer = (GameObject)Instantiate (bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);

			tracer.GetComponent<Rigidbody> ().velocity = bulletSpawnPos.transform.right * tracerSpeed;

			NetworkServer.Spawn (tracer);

			Destroy (tracer, 0.5f);

			yield return shotDuration;

			laserLine.enabled = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelPunch.Controller;

namespace PixelPunch.Weapons{
	public class WeaponPistol : Weapon {

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

			RaycastHit hit;

			Vector3 rayOrigin = bulletSpawnPos.position;

			Debug.DrawRay (rayOrigin, bulletSpawnPos.right * weaponRange, Color.green);

		//	laserLine.SetPosition (0, rayOrigin);

			if (Physics.Raycast (rayOrigin, bulletSpawnPos.right, out hit, weaponRange)) {
		//		laserLine.SetPosition (1, hit.point);
				var health = hit.collider.GetComponent<PlayerHealth>();

				if (health != null) {
					health.TakeDamage (damage);
				}
		//	} else {
		//		laserLine.SetPosition (1, bulletSpawnPos.right * weaponRange);
			}

		}

		public void SetDamage (int d){
			damage = d;
		}

		private IEnumerator ShotEffect(){
			laserLine.enabled = true;

			yield return shotDuration;

			laserLine.enabled = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PixelPunch.Controller;

namespace PixelPunch.Weapons {
	public class Weapon : MonoBehaviour {

		public GameObject bulletPrefab;
		public Transform bulletSpawnPos;
		protected int damage;
		protected float bulletSpeed;

		public virtual void Shoot(){
			var bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);

			bullet.GetComponent<Rigidbody> ().velocity = bulletSpawnPos.transform.right * bulletSpeed;

			NetworkServer.Spawn (bullet);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PixelPunch.Controller;

namespace PixelPunch.Weapons {
	public abstract class Weapon : NetworkBehaviour {

		public GameObject bulletPrefab;
		public Transform bulletSpawnPos;
		protected int damage = 10;
		protected float bulletSpeed = 6.0f;

		void Start(){
			//bulletSpawnPos = this.gameObject.transform;
		}
		void Update(){
			Debug.Log (bulletSpawnPos);
		}
			
		public virtual void Shoot(){
			var bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);

			bullet.GetComponent<Rigidbody> ().velocity = bulletSpawnPos.transform.right * bulletSpeed;

			NetworkServer.Spawn (bullet);
		}
	}
}

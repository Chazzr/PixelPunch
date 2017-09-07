using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PixelPunch.Weapons;

namespace PixelPunch.Controller {
	public class PlayerController : NetworkBehaviour {

		public Transform playerTransform;
		public Transform jumpTransform;
		public Rigidbody myRigidbody;
		public Transform weaponJoint;
		public Transform weaponSocket;
		public GameObject objectPrefab;
		public Transform objectSpawn;
		public GameObject startingWeapon;
		public Weapon weaponControl;

		public float throwSpeed;
		public float moveSpeed;
		public float jumpHeight;
		//public float bulletSpeed;

		private GameObject equippedWeapon;
		private float distanceToGround = 0.2f;
		private Vector3 worldPos;
		private Vector2 mousePos;

		void Start () { 
			equippedWeapon = startingWeapon;
			Instantiate (equippedWeapon, weaponSocket);
			weaponControl = equippedWeapon.GetComponent<Weapon> ();
		}
			
		private bool bIsGrounded(){
			return Physics.Raycast(jumpTransform.position, Vector3.down, distanceToGround);
		}
			
		void Update () {

			if (!isLocalPlayer) {

				return;
			}

			AimWeapon ();

			Vector3 playerPosition = playerTransform.position;

			//Debug for checking Jump threshold
			/*Vector3 forward = playerTransform.TransformDirection(Vector3.down) *distanceToGround;
				Debug.DrawRay(playerTransform.position, forward, Color.green);*/

			if (Input.GetButton ("Horizontal")) {

				if (Input.GetAxis ("Horizontal") > 0) {

					playerPosition.x += moveSpeed * Time.deltaTime;

					playerTransform.position = playerPosition;
				}
				if (Input.GetAxis ("Horizontal") < 0) {

					playerPosition.x += -moveSpeed * Time.deltaTime;

					playerTransform.position = playerPosition;
				}
			}

			//Checks to see if Player is within jump threshold. 
			if (Input.GetButtonDown ("Jump") && bIsGrounded()){
				Jump();
			}

			if (Input.GetButtonDown ("Fire1")) {
				CmdFire ();
			}
				
			if (Input.GetKeyDown (KeyCode.F)) {
				ObtainWorldMousePosition ();

				//print (worldPos + ":" + mousePos);

				ThrowGrenade();
			}
		}

		void Jump(){
			myRigidbody.AddForce (transform.up * jumpHeight, ForceMode.Impulse);
		}

		[Command]
		void CmdFire(){
			Debug.Log ("Firing Weapon!");

			weaponControl.Shoot ();
			//Vector3 bulletDir = new Vector3();

			//var bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

			//bullet.GetComponent<Rigidbody> ().velocity = bulletSpawn.transform.right * bulletSpeed;

			//NetworkServer.Spawn (bullet);
			//Destroy (bullet, 2.0f);
		}
			
		void AimWeapon(){
			Vector3 mousePosition = new Vector3 ();
			Camera  c = Camera.main;

			mousePosition = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

			weaponJoint.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (mousePosition.y, mousePosition.x) * Mathf.Rad2Deg);
		}

		void ThrowGrenade(){
			Vector3 grenadeDir = new Vector3 ();
			Vector3 grenadeDirNormal = new Vector3 ();

			grenadeDir = worldPos - objectSpawn.position;
			grenadeDirNormal = grenadeDir.normalized;

			Debug.Log (grenadeDirNormal);

			var grenade = (GameObject)Instantiate (objectPrefab, objectSpawn.position, objectSpawn.rotation);

			grenade.GetComponent<Rigidbody> ().velocity = grenadeDirNormal * throwSpeed;

			Destroy (grenade,2.0f);
		}

		void ObtainWorldMousePosition(){

			Vector3 p = new Vector3();
			Camera  c = Camera.main;
			Vector2 mouse = new Vector2();

			// Note that the y position from Event is inverted.
			mouse.x = Input.mousePosition.x;
			mouse.y = Input.mousePosition.y;

			p = c.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 10));

			worldPos = p;
			mousePos = mouse;

		}

		public override void OnStartLocalPlayer(){
			GetComponentInChildren<MeshRenderer> ().material.color = Color.blue;	
		}
	}
}

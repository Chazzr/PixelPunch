using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PixelPunch.Weapons;

namespace PixelPunch.Controller {
	public class PlayerBehavior: NetworkBehaviour {

		public Transform playerTransform;
		public Transform jumpTransform;
		public Rigidbody myRigidbody;
		public Transform weaponJoint;
		public Transform weaponSocket;
		public GameObject objectPrefab;
		public Transform objectSpawn;
		public GameObject startingWeapon;
		public Vector3 mousePosition = new Vector3();

		public float throwSpeed;
		public float moveSpeed;
		public float jumpHeight;
		public static Vector3 weaponOrigin;

		public Weapon _weaponControl;
		private GameObject _equippedWeapon;
		private float distanceToGround = 0.2f;
		private Vector3 worldPos;
		private Vector2 mousePos;
			
		private bool bIsGrounded(){
			return Physics.Raycast(jumpTransform.position, Vector3.down, distanceToGround);
		}
			
		void Start () { 
			SetWeaponOrigin (weaponJoint);
			_equippedWeapon = Instantiate (startingWeapon, weaponSocket) as GameObject;
			_weaponControl = _equippedWeapon.GetComponent<Weapon> ();
		}

		void Update () {

			if (!isLocalPlayer) {

				return;
			}
				
			AimWeapon ();

			SetWeaponOrigin (weaponJoint);

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

		void SetWeaponOrigin(Transform origin){
			weaponOrigin = origin.position;
		}

		void Jump(){
			myRigidbody.AddForce (transform.up * jumpHeight, ForceMode.Impulse);
		}

		[Command]
		void CmdFire(){
			Debug.Log ("Firing Weapon!");

			_weaponControl.Shoot ();
		}
			
		void AimWeapon(){
			Camera  c = Camera.main;

			mousePosition = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

			weaponJoint.LookAt (mousePosition, Vector3.forward);
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

			p = c.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 5));

			worldPos = p;
			mousePos = mouse;

		}

		public override void OnStartLocalPlayer(){
			//GetComponentInChildren<MeshRenderer> ().material.color = Color.blue;	
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PixelPunch.Controller;
using PixelPunch.Weapons;
using PixelPunch.Controller;

namespace PixelPunch.Weapons{
	public class ReticleBehavior : NetworkBehaviour {

		public GameObject reticlePrefab;				//reference to 3D reticle Prefab
		public GameObject spawnedReticleCenter;				//reference to instantiated Prefab
		public GameObject spawnedReticleUpper;
		public GameObject spawnedReticleLower;
		public static Vector2 aimOrigin2dPosition;		//reference to 2D weapon rotator position in screenspace
		public static Vector2 aimTarget2dPosition;		//referemce to 2D reticle position in screenspace
		public float weaponAccuracy = 1.0f;
		public float offsetOutSpeed = 10.0f;
		public float offsetReturnSpeed = 2.0f;

		private Vector3 _aimOffsetUpperOrigin;
		private Vector3 _aimOffsetUpperSaved;
		private bool _bHasReachedLimit = false;


		void Start () {

			offsetOutSpeed = 10.0f;

			spawnedReticleCenter = Instantiate (reticlePrefab) as GameObject;
			spawnedReticleUpper = Instantiate (reticlePrefab, spawnedReticleCenter.transform) as GameObject;
			spawnedReticleLower = Instantiate (reticlePrefab, spawnedReticleCenter.transform) as GameObject;

			_aimOffsetUpperOrigin = spawnedReticleUpper.transform.localPosition;
		}
		

		void Update () {

			spawnedReticleCenter.transform.position = gameObject.GetComponentInChildren<PlayerBehavior> ().mousePosition;

			spawnedReticleCenter.transform.LookAt (PlayerBehavior.weaponOrigin);
			spawnedReticleUpper.transform.LookAt (PlayerBehavior.weaponOrigin);

			ConvertWorldAimDirection ();
		//	ConvertWorldAimOrigin ();
		//	Debug.Log (PlayerBehavior.weaponOrigin);

			if (Input.GetButtonDown ("Fire1")) {
				OffsetAim (weaponAccuracy);
				Debug.Log ("Offset Aim!");
			}

			Debug.Log ("Checkpoint");

			if (_aimOffsetUpperSaved != _aimOffsetUpperOrigin) {

				//If saved upper aim threshold position has not been reached
				if (!_bHasReachedLimit) {

					//Locally transform upper reticle upwards.
					Vector3 tempA = spawnedReticleUpper.transform.localPosition;
					tempA.y += offsetOutSpeed * Time.deltaTime;
					spawnedReticleUpper.transform.localPosition = tempA;

					//If local Y position has reached the upper aim threshold position.
					//Set upper reticle local position equal to saved upper aim threshold position.
					//Upper aim threshold position has been reached.
					if (spawnedReticleUpper.transform.localPosition.y >= _aimOffsetUpperSaved.y && !_bHasReachedLimit) {
						spawnedReticleUpper.transform.localPosition = _aimOffsetUpperSaved;
						_bHasReachedLimit = true;
					}
				}

				//If saved upper aim threshold position has been reached
				if (spawnedReticleUpper.transform.localPosition.y <= _aimOffsetUpperSaved.y && _bHasReachedLimit) {

					//Locally transform upper reticle downwards.
					Vector3 tempB = spawnedReticleUpper.transform.localPosition;
					tempB.y += -offsetReturnSpeed * Time.deltaTime;
					spawnedReticleUpper.transform.localPosition = tempB;

					//If local Y position has returned to the upper threshold origin
					//Set upper reticle local position equal to upper aim threshold origin
					//Set saved upper aim threshold back to origin
					//Reset ReachedLimit bool back to false
					if (spawnedReticleUpper.transform.localPosition.y <= _aimOffsetUpperOrigin.y) {
						spawnedReticleUpper.transform.localPosition = _aimOffsetUpperOrigin;
						_aimOffsetUpperSaved = _aimOffsetUpperOrigin;
						_bHasReachedLimit = false;
					}
				}
			}
		}

		void ConvertWorldAimDirection(){

			aimTarget2dPosition = Camera.main.WorldToScreenPoint (spawnedReticleCenter.transform.position);
		}

		void OffsetAim(float accuracy){
			Vector3 offset = spawnedReticleUpper.transform.localPosition;

			offset.y += accuracy;

			_aimOffsetUpperSaved = offset;
		}

		//void ConvertWorldAimOrigin(){
		//	aimOrigin2dPosition = Camera.main.WorldToScreenPoint (aimOrigin3dPosition);
		//}
	}
}

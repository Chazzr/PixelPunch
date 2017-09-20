using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	private Animator anim;

	public float ikWeight = 1.0f;
	public Transform leftHandIKTarget;

	private float running;


	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		running = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

			running = Input.GetAxis ("Horizontal");
			anim.SetFloat ("running", running);

	}

	void OnAnimatorIK(){
		anim.SetIKPositionWeight (AvatarIKGoal.LeftHand, ikWeight);

		anim.SetIKPosition (AvatarIKGoal.LeftHand, leftHandIKTarget.position);
	}
}

using System.Collections;
using UnityEngine;

public class RaycastShootComplete : MonoBehaviour {

	public int gunDamage = 1;												//Set the number of hitpoints this gun will take away from shot objects
	public float fireRate = 0.25f;											//The number in seconds that controls how often the player can fire
	public float weaponRange = 50f;											//Distance in Unity units over which the player can fire
	public float hitForce = 100f;											//Amount of force which will be added to objects with a rigid body that are shot by this gun
	public Transform gunEnd;												//Holds a reference to the end of gun location. This marks the muzzle's location.

	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);		// WaitForSeconds object used by our ShotEffect coroutine, determines time last shot
	private LineRenderer laserLine;
	private float nextFire;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

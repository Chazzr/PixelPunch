using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelPunch.Weapons;

public class UserInterfaceBehavior : MonoBehaviour {

	public Vector2 aimArrowLocation;
	public Vector2 aimOriginLocation;
	public RectTransform aimArrowImage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		aimArrowLocation = ReticleBehavior.aimTarget2dPosition;
		aimArrowImage.anchoredPosition = aimArrowLocation;
	}
}

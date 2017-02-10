using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeDial : MonoBehaviour 
{
	public GameObject laserEnd;
	public float dialRotNorm;
	float currentRot;
	float minRot = 0f;
	float maxRot = 180f;
	float startAngle;
	private Quaternion originalRotation;
	private Quaternion newRotation;
	Vector3 laserHitPoint;
	public GameObject mouth;
	AudioSource audio;

	//instead of custom events for laser, use static bools for now.. laserOver, laserHeld
	// would custom events for the laser be helpful?? find/figure out.
	public static bool laserOver;
	public static bool laserHeld;
		

	// Use this for initialization
	void Start () {
		audio = mouth.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		currentRot = transform.localEulerAngles.y;
		float dialRotNorm = Mathf.InverseLerp (minRot, maxRot, currentRot);
		audio.volume = dialRotNorm;

		if(laserOver = true){
			Transform laserHit = laserEnd.GetComponent<Transform>();
			laserHitPoint = laserEnd.transform.position;

			//if laser is hovering, set the start angle
			startAngle = Mathf.Atan2(laserHitPoint.y, laserHitPoint.x) * Mathf.Rad2Deg;
			originalRotation = this.transform.rotation;

		}

		if (laserHeld = true) {
			//calculate new rotation for dial, based on movement of laser hit point object
			Transform laserHit = laserEnd.GetComponent<Transform>();
			laserHitPoint = laserEnd.transform.position;
			float endAngle = Mathf.Atan2 (laserHitPoint.y, laserHitPoint.x) * Mathf.Rad2Deg;
			Quaternion newRotation = Quaternion.AngleAxis (endAngle - startAngle, this.transform.up);
			newRotation.z = 0; 
			newRotation.eulerAngles = new Vector3 (0, -newRotation.eulerAngles.y, 0);
			this.transform.rotation = originalRotation * (newRotation);

			//set current rotation, so it can be used to adjust the volume in update loop
			currentRot = transform.localEulerAngles.y;
			currentRot = Mathf.Clamp (transform.eulerAngles.y, 0, 180);
		}
	}
}

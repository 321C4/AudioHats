using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotateThing : MonoBehaviour {

	private Quaternion originalRotation;
	private float startAngle = 0;

	public void Start()
	{
		originalRotation = this.transform.rotation;
	}
	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ButtonDown();
		}
		if (Input.GetMouseButton(0))
		{
			ButtonHeld();
		}
	}

	// use on controller button down
	public void ButtonDown()
	{
		//save the starting rotation for the dial
		originalRotation = this.transform.rotation;
		//maps transform point of dial to point on screen
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		//create vector from mouse point to dial transform on screen
		Vector3 vector = Input.mousePosition - screenPos;
		//with laser pointer, this will be done with a raycast!

		//record start angle, before mouse starts dragging
		startAngle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
	}

	// use on controller button held
	public void ButtonHeld()
	{
		// in vive:
		//raycast laserpointer to a plane parallel to the dial collider, with empty object at end of ray.
		//only account for movement of laser across the dial mesh collider: user can raycast from whatever angle/position
		//do angle deltas LOCALLY 
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 vector = Input.mousePosition - screenPos;


		float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
		Quaternion newRotation = Quaternion.AngleAxis(angle - startAngle, this.transform.up);
		newRotation.z = 0; 
		newRotation.eulerAngles = new Vector3(0, -newRotation.eulerAngles.y, 0);
		this.transform.rotation = originalRotation * (newRotation);
	}
}

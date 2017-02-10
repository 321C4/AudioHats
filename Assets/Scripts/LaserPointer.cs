using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

[RequireComponent(typeof(LineRenderer))]

//Attach this script to the controllers

public class LaserPointer : MonoBehaviour 
{
	public Color LineColor;
	public float LineWidth = 0.02f;
	public bool ForceLineVisible = true;

	public bool OnlyVisibleOnTouch = true;

	private LineRenderer Line;

	private NVRHand Hand;

	//empty GameObject for collision detection on the UI objects
	new GameObject LaserEnd;
	private Vector3 endPoint;

	//instead of custom events for laser, use static bools for now.. laserOver, laserHeld
	public static bool laserOver;
	public static bool laserHeld;

	private void Awake()
	{
		//add a sphere collider to the end of the laser
		SphereCollider sc = LaserEnd.AddComponent(typeof(SphereCollider)) as SphereCollider;
		sc.radius = 0.1f;

		Line = this.GetComponent<LineRenderer>();
		Hand = this.GetComponent<NVRHand>();

		if (Line == null)
		{
			Line = this.gameObject.AddComponent<LineRenderer>();
		}

		if (Line.sharedMaterial == null)
		{
			Line.material = new Material(Shader.Find("Unlit/Color"));
			Line.material.SetColor("_Color", LineColor);
			NVRHelpers.LineRendererSetColor(Line, LineColor, LineColor);
		}

		Line.useWorldSpace = true;
	}

	private void LateUpdate()
	{
		laserHeld = false;
		laserOver = true;

		Line.enabled = ForceLineVisible || (OnlyVisibleOnTouch && Hand != null && Hand.Inputs[NVRButtons.Touchpad].IsTouched);

		if (Line.enabled == true)
		{
			Line.material.SetColor("_Color", LineColor);
			NVRHelpers.LineRendererSetColor(Line, LineColor, LineColor);
			NVRHelpers.LineRendererSetWidth(Line, LineWidth, LineWidth);

			RaycastHit hitInfo;
			bool hit = Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, 1000);

			if (hit == true)
			{   
				if (Hand.Inputs [NVRButtons.Touchpad].IsTouched) {
					endPoint = hitInfo.point;
					LaserEnd.transform.position = endPoint;
					laserOver = true;
				}

				else if (Hand.Inputs [NVRButtons.Touchpad].IsPressed) {
					laserHeld = true;
					endPoint = hitInfo.point;
					LaserEnd.transform.position = endPoint;

				}
			}
			else
			{
				endPoint = this.transform.position + (this.transform.forward * 1000f);
			}

			Line.SetPositions(new Vector3[] { this.transform.position, endPoint });
		}
	}
}

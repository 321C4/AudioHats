using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragforDebug : MonoBehaviour {
	private bool dragging = false;
	// Use this for initialization

	void OnMouseDown(){
		dragging = true;
	}

	void OnMouseUp(){
		dragging = false;
	}

	
	// Update is called once per frame
	void Update () {
		Plane plane = new Plane (Vector3.up, new Vector3 (0, 0, 0));
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float distance;
		if (dragging) {
			if (plane.Raycast (ray, out distance)) {
				transform.position = ray.GetPoint (distance);
				Debug.Log (distance);
			}
		}
	}
}

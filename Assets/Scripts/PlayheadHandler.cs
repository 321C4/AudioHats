using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadHandler : MonoBehaviour {
	private bool dragging = false;
	public GameObject songPlank;
	private AudioSource PlankAudio;

	// Use this for initialization

	void Start(){
		transform.localPosition = new Vector3 
			(transform.localPosition.x, transform.localPosition.y, 
				(AudioScrub.plankHeight / 2));
	}

	void OnMouseDown(){
		dragging = true;
		Debug.Log (dragging);
	}

	void OnMouseUp(){
		dragging = false;
		Debug.Log (dragging);
	}


	// Update is called once per frame
	void Update () {
		Plane plane = new Plane (Vector3.forward, new Vector3 (0, 0, 0));
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float distance;
		if (dragging) {
			if (plane.Raycast (ray, out distance)) {
				transform.position = ray.GetPoint (distance);
			}
		}
	}                                              
}

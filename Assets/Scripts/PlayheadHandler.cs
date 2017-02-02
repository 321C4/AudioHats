using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadHandler : MonoBehaviour {
	public static bool dragging = false;
	public GameObject songPlank;
	private AudioSource PlankAudio;
	private float clipLength = 0f;
	// Use this for initialization

	void Start(){
		transform.localPosition = new Vector3 
			(transform.localPosition.x, transform.localPosition.x, 
				(AudioScrub.plankHeight / 2));
	}

	void OnMouseDown(){
		dragging = true;
	}

	void OnMouseUp(){
		dragging = false;
	}


	// Update is called once per frame
	void Update () {
		Debug.Log (transform.localPosition.x);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadHandler : MonoBehaviour {
	private bool dragging = false;
	public GameObject songPlank;
	private AudioSource PlankAudio;
	Vector3 pos;

	// Use this for initialization

	void Start(){
		
		transform.localPosition = new Vector3  
			(transform.localPosition.x, transform.localPosition.y, 
				(AudioScrub.plankHeight / 2));
		transform.localPosition = pos;
		AudioScrub.knobPosZ = pos.z;
	}


	// Update is called once per frame
	void DragKnob () {

	}                                              
}

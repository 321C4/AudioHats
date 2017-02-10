using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendPitch : MonoBehaviour {
	
	public GameObject SliderTrack;
	AudioSource audio;
	float curClipLength;
	float startingClipLength;
	public Transform sliderStart;
	public Transform sliderEnd;
	private float sliderLength;

	//bools for controlling state
	private bool dragging = false;
	public bool playing;

	private Rigidbody rb;

	float KnobCurVel;

	float playHeadAccel;
	Vector3 OldXpos;
	Vector3 NewXpos;
	Vector3 DragKnobPos;
	float KnobXnorm;


	// Use this for initialization
	void Start () {
		//get info for starting variables
		dragging = false;

		audio = SliderTrack.GetComponent<AudioSource> ();
		curClipLength = audio.clip.length;
		startingClipLength = audio.clip.length; // curClipLength will change as pitch changes! e.g. 0.5 pitch = 2x length!

		rb = gameObject.GetComponent<Rigidbody> ();
		sliderLength = (sliderEnd.position.x - sliderStart.position.x);
		transform.localPosition = new Vector3 ((sliderStart.position.x), 
			transform.localPosition.y, transform.localPosition.z);
		
		//need to add a toggle control for play/pause audio
		audio.Play ();
	}

	void OnMouseDown(){
		// apply dragging in the update loop
		dragging = true;

	}

	void OnMouseUp(){

		dragging = false;
		//stop the dragging behavior in the update loop
		Debug.Log ("not dragging!");
		//reset the audio pitch to 1, which resets clip length to it's original value
		audio.pitch = 1;
		curClipLength = startingClipLength;
		// set knob position based on current time in audio clip
		transform.localPosition = new Vector3 (((sliderStart.position.x) + ((audio.time * sliderLength) / startingClipLength)), 
			transform.localPosition.y, transform.localPosition.z);
		playing = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// note the transform of this knob gameObject at the beginning of the frame
		OldXpos = transform.localPosition;


		//if mouse is clicked
		if (dragging) {
			
			//define a plane and raycast to it from the camera, through the mouse
			//hit point is mouse click point
			Plane plane = new Plane (Vector3.back, new Vector3 (0, 0, -0.5f));
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float distance;			

			//if the plane is being hit by the raycast
			if (plane.Raycast (ray, out distance)) {

				//calculate new position for knob, based on mouse position on screen
				NewXpos = ray.GetPoint (distance);
				NewXpos.y = 0;
				NewXpos.x = Mathf.Clamp (NewXpos.x, sliderStart.position.x, sliderEnd.position.x);
				//set position of knob
				transform.position = NewXpos;
				//set the current time of clip based on new position of knob
				audio.time = 2 *(transform.localPosition.x - sliderStart.localPosition.x) * (startingClipLength / sliderLength);


				//find knob velocity for this frame
				KnobCurVel = (Vector3.Distance(OldXpos, NewXpos) / Time.deltaTime);

				// calculate new pitch based on knob velocity across slider
				audio.pitch = (startingClipLength * KnobCurVel) / sliderLength;
				//set clipLength var to length based on current pitch
				curClipLength = audio.clip.length / Mathf.Abs(audio.pitch);

			}
		}

		else if (playing = true)  {
	
			
			//move the knob along the slider proportionally in time with the clip
			transform.localPosition = new Vector3 (((sliderStart.position.x) + ((audio.time * sliderLength) / startingClipLength)), 
				transform.localPosition.y, transform.localPosition.z);

			//find knob velocity for this frame
			NewXpos = transform.localPosition;
			KnobCurVel = (Vector3.Distance(OldXpos, NewXpos) / Time.deltaTime);
		} 

		Debug.Log ("clipLength = " + curClipLength + ", current clip time: " + audio.time + ", current knob vel = " + KnobCurVel);
	}
		//PlayheadPos = (transform.localPosition.x)-(sliderStart.position.x);
		//Playhead normalized = Mathf.InverseLerp(sliderStart, sliderEnd, PlayheadPos);
}


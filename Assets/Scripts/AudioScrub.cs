using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScrub : MonoBehaviour {

	public GameObject songPlank;
	public GameObject scrubberKnob;
	public GameObject[] ColorGos;
	private float knobPosX = 0f;
	private float knobPosZ;
	private Vector3 newKnobPos;
	private AudioSource PlankAudio;
	public List<GameObject> colorChannels;
	public Material lerpMat1;
	public Material lerpMat2;
	public Renderer rend;
	private float clipLength = 0f; //lenght of audioclip
	private float plankHeight;
	private float plankLength;
	private float playheadTime;
	new bool isPaused = false;

	private void Start()
	{   
		knobPosZ = scrubberKnob.transform.position.z;
		rend = songPlank.GetComponent<Renderer>();
		PlankAudio = songPlank.GetComponent<AudioSource>(); 
		clipLength = PlankAudio.clip.length; // Length of the audioclip attached to this song plank
		PlankAudio.loop = true;
		plankHeight = (rend.bounds.max.z)-(rend.bounds.min.z);
		plankLength = (rend.bounds.max.x)-(rend.bounds.min.x);
		Debug.Log (plankLength);
	}
		

	void Update(){
		scrubberKnob.transform.localPosition = new Vector3 
			(((PlankAudio.time*plankLength)/clipLength), scrubberKnob.transform.localPosition.y, 
			scrubberKnob.transform.localPosition.z);
		knobPosX = scrubberKnob.transform.localPosition.x;
		rend.material.Lerp(lerpMat1, lerpMat2,(knobPosX/plankLength));
		Debug.Log (knobPosX);

	}
	//public Vector3 PlayheadPosition(float knobPos, float volumePos){
	//}
}

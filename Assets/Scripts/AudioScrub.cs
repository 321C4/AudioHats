using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScrub : MonoBehaviour {
	public GameObject songPlank;
	public GameObject scrubberKnob;
	public static float knobPosZ;
	public static float knobPosX;
	private AudioSource PlankAudio;
	public List<GameObject> colorKnobs;
	public Material lerpMat1;
	public Material lerpMat2;
	public Renderer rend;
	private float clipLength = 0f; //lenght of audioclip
	public static float plankHeight;
	public static float plankLength;
	new bool isPaused = false;

	private void Start()
	{   
		rend = songPlank.GetComponent<Renderer>();
		PlankAudio = songPlank.GetComponent<AudioSource>(); 
		clipLength = PlankAudio.clip.length; // Length of the audioclip attached to this song plank
		PlankAudio.loop = true;
		plankHeight = (rend.bounds.max.z)-(rend.bounds.min.z);
		plankLength = (rend.bounds.max.x)-(rend.bounds.min.x);

	}

	void Update(){
		knobPosX = scrubberKnob.transform.localPosition.x;
		scrubberKnob.transform.localPosition = new Vector3 (((PlankAudio.time * plankLength) / clipLength), 
			scrubberKnob.transform.localPosition.y, scrubberKnob.transform.localPosition.z);
		
		rend.material.Lerp (lerpMat1, lerpMat2, (knobPosX / plankLength));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required when Using UI elements.

public class AudioScrub : MonoBehaviour {
	public Slider mainSlider;
	public GameObject songPlank;
	public GameObject scrubberKnob;
	public GameObject[] ColorGos;
	private float knobPosZ;
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
		knobPosZ = scrubberKnob.transform.position.z;
		rend = songPlank.GetComponent<Renderer>();
		PlankAudio = songPlank.GetComponent<AudioSource>(); 
		PlankAudio.volume = 0.5f;
		clipLength = PlankAudio.clip.length; // Length of the audioclip attached to this song plank
		PlankAudio.loop = false;
		plankHeight = (rend.bounds.max.z)-(rend.bounds.min.z);
		plankLength = (rend.bounds.max.x)-(rend.bounds.min.x);
	}
		

	void Update(){

			scrubberKnob.transform.localPosition = new Vector3 (((PlankAudio.time * plankLength) / clipLength), 
				scrubberKnob.transform.localPosition.y, scrubberKnob.transform.localPosition.z);
		
		rend.material.Lerp (lerpMat1, lerpMat2, (PlayheadHandler.knobPosX / plankLength));
		PlankAudio.volume = (scrubberKnob.transform.localPosition.z)/plankHeight;

	}
}

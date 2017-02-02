using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySongDebug : MonoBehaviour {
	public GameObject plank;
	private AudioSource PlankAudio;
	private bool isPlaying = false;
	// Use this for initialization
	void Start () {
		PlankAudio= plank.GetComponent<AudioSource>();
	}
	
	void OnMouseDown(){
		if (PlankAudio.isPlaying) {
			PlankAudio.Stop ();
			isPlaying = false;
		} 
		else {
			PlankAudio.Play ();
			isPlaying = true;
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySongDebug : MonoBehaviour {
	private AudioSource PlankAudio;
	private bool isPlaying = false;
	// Use this for initialization
	void Start () {
		PlankAudio= gameObject.GetComponent<AudioSource>();
	}
	
	void OnMouseDown(){
		if (PlankAudio.isPlaying) {
			PlankAudio.Stop ();
		} 
		else {
			PlankAudio.Play ();
		}
	}

}

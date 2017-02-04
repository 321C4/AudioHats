using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySongDebug : MonoBehaviour {
	public GameObject plank;
	private AudioSource PlankAudio;
	// Use this for initialization
	void Start () {
		PlankAudio= plank.GetComponent<AudioSource>();
	}
	
	public void PlayTheMusic(){
		if (PlankAudio.isPlaying) {
			PlankAudio.Stop ();
		}

		else {
			PlankAudio.Play ();
		}
	}

}

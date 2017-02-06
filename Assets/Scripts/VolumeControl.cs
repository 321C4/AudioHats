using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  NewtonVR{

public class VolumeControl : MonoBehaviour {
	public NVRSlider Slider;
	public AudioSource PlankAudio;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlankAudio.volume = (Slider.CurrentValue);
	}
}
}
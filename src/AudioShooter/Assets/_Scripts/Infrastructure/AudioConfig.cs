using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioConfig : MonoBehaviour {

	public static AudioConfig Instance { get; private set; }
	public AudioSource Music { get; private set; }

	void Awake () {
		Music = GetComponent<AudioSource>();
	
		Instance = this;
	}

}
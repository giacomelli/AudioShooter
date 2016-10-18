using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioConfig : MonoBehaviour {

	public static AudioConfig Instance { get; private set; }
	public AudioSource MusicSource { get; private set; }
	public float MusicDuration { get; private set; }

	void Awake () {
		MusicSource = GetComponent<AudioSource>();
		MusicDuration = MusicSource.clip.length;
		Instance = this;
	}

}
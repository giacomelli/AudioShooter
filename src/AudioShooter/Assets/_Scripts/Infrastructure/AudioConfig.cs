using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioConfig : MonoBehaviour {

	public static AudioConfig Instance { get; private set; }
	public AudioSource MusicSource { get; private set; }
	public float MusicDuration { get; private set; }
	public Text MusicLabel;

	void Awake () {
		MusicSource = GetComponent<AudioSource>();
		MusicDuration = MusicSource.clip.length;
		Instance = this;

		MusicLabel.text = MusicSource.clip.name;
	}
}
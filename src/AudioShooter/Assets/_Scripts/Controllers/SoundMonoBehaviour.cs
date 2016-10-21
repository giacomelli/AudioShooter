using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SoundConfig))]
public class SoundMonoBehaviour : MonoBehaviour {

	protected SoundConfig Config { get; private set; }

	// Use this for initialization
	void Awake () {
		Config = GetComponent<SoundConfig>();
	}

	public float AudioBuffer
	{
		get
		{
			return AudioAnalysisService.Instance.AudioBandBuffer[Config._band];
		}
	}
}
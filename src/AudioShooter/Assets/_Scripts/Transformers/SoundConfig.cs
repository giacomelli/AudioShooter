using UnityEngine;
using System.Collections;
using System;

[DisallowMultipleComponent]
public class SoundConfig : MonoBehaviour {

	[Range(0, 7)]
	public int _band;
	public bool _randomBand;

	void Start() {
		if (_randomBand)
		{
			_band = RandomBand();
		}
	}

	public static int RandomBand()
	{
		return UnityEngine.Random.Range(0, 7);
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightController: SoundMonoBehaviour
{
	public int _minIntensity, _maxIntensity;
	Light _light;

	// Use this for initialization
	void Start()
	{
		_light = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update()
	{
		_light.intensity = (AudioRealtimeService.Instance.AudioBandBuffer[Config._band] * (_maxIntensity - _minIntensity)) + _minIntensity;
	}
}

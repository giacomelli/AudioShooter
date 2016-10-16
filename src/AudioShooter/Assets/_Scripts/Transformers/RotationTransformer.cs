using UnityEngine;
using System.Collections;

public class RotationTransformer : SoundMonoBehaviour {
	public Vector3 _minRotation;
	public Vector3 _maxRotation;
	public bool RandomRotation;

	Vector3 _rotationRange;

	// Use this for initialization
	void Start () {

		if (RandomRotation)
		{
			_minRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
			_maxRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
		}

		_rotationRange = _maxRotation - _minRotation;
	}
	
	// Update is called once per frame
	void Update () {

		var bandBuffer = AudioAnalysisService.AudioBandBuffer[Config._band];
		var eulerAngles = transform.eulerAngles;
		var newEulerAngles = new Vector3(
			_rotationRange.x == 0 ? eulerAngles.x : _rotationRange.x * bandBuffer,
			_rotationRange.y == 0 ? eulerAngles.y : _rotationRange.y * bandBuffer,
			_rotationRange.z == 0 ? eulerAngles.z : _rotationRange.z * bandBuffer);

		transform.eulerAngles = newEulerAngles;
	}
}

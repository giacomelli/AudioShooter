using UnityEngine;
using System.Collections;

public class PositionTransformer : SoundMonoBehaviour {
	public Vector3 _minPosition;
	public Vector3 _maxPosition;

	Vector3 _positionRange;

	void Start () {
		_positionRange = _maxPosition - _minPosition;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = AudioService.AudioBandBuffer[Config._band] * _positionRange + _minPosition;
	}
}

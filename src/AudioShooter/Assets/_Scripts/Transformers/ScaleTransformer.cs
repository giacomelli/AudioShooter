using UnityEngine;
using System.Collections;

public class ScaleTransformer : SoundMonoBehaviour {
	public Vector3 _minScale;
	public Vector3 _maxScale;
	Vector3 _scaleRange;

	// Use this for initialization
	void Start () {
		_scaleRange = _maxScale - _minScale;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = AudioRealtimeService.Instance.AudioBandBuffer[Config._band] * _scaleRange + _minScale;
	}
}

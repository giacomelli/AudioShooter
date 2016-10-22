using UnityEngine;
using System.Collections;

public class RotationTransformer : SoundMonoBehaviour {
	public Vector3 _minRotation;
	public Vector3 _maxRotation;
	public bool RandomRotation;

	Vector3 _rotationRange;

	void Start () {

		if (RandomRotation)
		{
			_minRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
			_maxRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
		}

		_rotationRange = _maxRotation - _minRotation;
	}
	
	void Update () {

		var metric = BehaviourMetric;
	
		var eulerAngles = transform.eulerAngles;
		var newEulerAngles = new Vector3(
			_rotationRange.x == 0 ? eulerAngles.x : _rotationRange.x * metric,
			_rotationRange.y == 0 ? eulerAngles.y : _rotationRange.y * metric,
			_rotationRange.z == 0 ? eulerAngles.z : _rotationRange.z * metric);

		transform.eulerAngles = newEulerAngles;
	}
}

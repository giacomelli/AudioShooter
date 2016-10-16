using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

	public float _velocity;
	Vector3 _startPosition;
	Vector3 _target;
	float _velocityFraction;
	Camera _camera;

	public static CameraController Instance { get; private set; }
	public float TopZ { get; private set; }

	void Awake()
	{
		_camera = GetComponent<Camera>();
		Instance = this;
	}

	void Start()
	{
		_startPosition = transform.position;
		_target = new Vector3(_startPosition.x, _startPosition.y, AudioAnalysisService.AudioDuration);
	}

	void Update()
	{
		if (AudioAnalysisService.AudioAnalyzed)
		{
			_velocityFraction += Time.deltaTime / AudioAnalysisService.AudioDuration;

			transform.position = Vector3.Lerp(_startPosition, _target, _velocityFraction);
			TopZ = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane)).z + _velocity + _velocityFraction;
		}
	}
}

using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

	public float _velocity;

	Vector3 _startPosition;
	Vector3 _target;
	float _velocityFraction;
    Camera _camera;
	Action _updateAction = () => { };

	public static CameraController Instance { get; private set; }
	public float TopZ { get; private set; }

	void Awake()
	{
		Instance = this;
		_camera = GetComponent<Camera>();
		AudioAnalysisService.Instance.Analyzed += delegate {
			_updateAction = () =>
			{
				_velocityFraction += Time.deltaTime / AudioConfig.Instance.MusicDuration;

				transform.position = Vector3.Lerp(_startPosition, _target, _velocityFraction);
				TopZ = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane)).z + _velocity + _velocityFraction;
			};
		};
	}

	void Start()
	{
		_startPosition = transform.position;
		_target = new Vector3(_startPosition.x, _startPosition.y, AudioConfig.Instance.MusicDuration);
	}

	void Update()
	{
		_updateAction();
	}
}

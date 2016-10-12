using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	Vector3 _target;
	Vector3 _direction;

	public float _velocity;
	public float _distance;

	public GameObject Shooter { get; private set; }

	public void Initialize(GameObject shooter, Vector3 direction)
	{
		Shooter = shooter;
		_direction = direction;
		_target = transform.position + (_direction * _distance);
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, _target, _velocity);
	}

	void OnTriggerEnter(Collider other)
	{
		if ((other.IsEnemy() && other.gameObject != Shooter) || other.IsMountain())
		{
			MissileAppService.DestroyMissile(gameObject);
		}
	}
}

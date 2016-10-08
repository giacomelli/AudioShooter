using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	Vector3 _target;
	public float _velocity;
	public float _distance;

	public Vector3 Direction { get; set; }
	public GameObject Shooter { get; set; }

	void Start()
	{
		_target = transform.position + (Direction * _distance);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, _target, _velocity);

		if (transform.position == _target)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if ((other.IsEnemy() && other.gameObject != Shooter) || other.IsMountain())
		{
			Destroy(gameObject);
		}
	}
}

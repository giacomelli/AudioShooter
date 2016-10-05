using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	Vector3 _target;
	public float _velocity;
	public float _distance;

	void Start()
	{
		_target = transform.right * _distance;
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
		if (other.tag == "Enemy")
		{
			Destroy(gameObject);
		}
	}
}

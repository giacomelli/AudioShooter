using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {
	Quaternion _rotationTarget;
	bool _isDead;
	public float _velocity;
	public float _dieDelay;
	public float _dieExplosionForce;
	
	void Start()
	{
		_rotationTarget = Quaternion.LookRotation(Vector3.zero - transform.position, Vector3.one);
	}
	 
	// Update is called once per frame
	void Update () {
		//transform.rotation = Quaternion.Lerp(transform.rotation, _rotationTarget, Time.deltaTime * _velocity);
		transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * _velocity);
	}
	 
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Missile")
		{
			StartCoroutine(Die());
		}
	}

	IEnumerator Die()
	{
		if (!_isDead)
		{
			_isDead = true;
			gameObject.GetComponent<Collider>().enabled = false;
			var rb = gameObject.AddComponent<Rigidbody>();
			rb.AddExplosionForce(_dieExplosionForce, Vector3.down, 10f);

			yield return new WaitForSeconds(_dieDelay);

			Destroy(gameObject);
		}
	}
}




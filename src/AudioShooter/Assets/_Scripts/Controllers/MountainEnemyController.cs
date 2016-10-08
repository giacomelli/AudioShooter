using UnityEngine;
using System.Collections;
using System;

public class MountainEnemyController : MonoBehaviour {
	bool _isDead;
	public float _dieDelay;
	public float _dieExplosionForce;

	[Range(0f, 1f)]
	public float _fireChance;

	public float _fireInterval;

	void Start()
	{
		StartCoroutine(Fire());
	}

	IEnumerator Fire()
	{
		while (true)
		{
			yield return new WaitForSeconds(_fireInterval);

			if (UnityEngine.Random.Range(0f, 1f) <= _fireChance)
			{
				MissileAppService.CreateMissile(gameObject, transform.position, transform.position.x < 0 ? Vector3.right : Vector3.left);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.IsMissile() && !other.IsMyMissile(this))
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




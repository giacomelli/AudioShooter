using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SoundConfig))]
public class EnemyControllerBase : SoundMonoBehaviour
{
	// Fields.
	bool _isDead;
	bool _canFire = true;
	Collider _collider;

	// Editor properties.
	public float _dieDelay;
	public float _dieExplosionForce;

	[Range(0f, 1f)]
	public float _minAudioBandToFire;
	public float _fireInterval;
	public float _missileVelocity;

	void Start()
	{
		_collider = gameObject.GetComponent<Collider>();
		PerformStart();
	}

	protected virtual void PerformStart()
	{
	}

	void OnEnable()
	{
		var rb = GetComponent<Rigidbody>();

		if (rb != null)
		{
			Destroy(rb);
			_collider.enabled = true;
		}

		_isDead = false;
	}


	void Update()
	{
		if (BehaviourMetric >= _minAudioBandToFire && !_isDead)
		{
			Fire();
		}
	}

	void Fire()
	{
		if (_canFire)
		{
			_canFire = false;
			PerformFire();
			StartCoroutine(ReleaseFire());
		}
	}

	protected virtual void PerformFire()
	{
	}

	IEnumerator ReleaseFire()
	{
		yield return new WaitForSeconds(_fireInterval);
		_canFire = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.IsMissile() && !other.IsEnemyMissile())
		{
			StartCoroutine(Die());
		}
	}

	IEnumerator Die()
	{
		if (!_isDead)
		{
			Score.Instance.RegisterEnemyKilled();
			StopCoroutine("ReleaseFire");

			_isDead = true;
			_collider.enabled = false;
			var rb = gameObject.AddComponent<Rigidbody>();
			rb.AddExplosionForce(_dieExplosionForce, Vector3.down, 10f);

			yield return new WaitForSeconds(_dieDelay);

			PerformDestroy();
		}
	}

	protected virtual void PerformDestroy()
	{
	}
}
﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SoundConfig))]
public class MountainEnemyController : SoundMonoBehaviour {
	bool _isDead;
	Bounds _bounds;
	bool _canFire = true;
	Collider _collider;

	public float _dieDelay;
	public float _dieExplosionForce;

	[Range(0f, 1f)]
	public float _minAudioBandToFire;

	public float _fireInterval;
	public float _missileVelocity;

	void Start()
	{
		_bounds = GetComponent<MeshFilter>().mesh.bounds;
		_collider = gameObject.GetComponent<Collider>();
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
		if (BehaviourMetric >= _minAudioBandToFire)
		{
			Fire();
		}
	}

	IEnumerator ReleaseFire()
	{
		yield return new WaitForSeconds(_fireInterval);
		_canFire = true;
	}

	void Fire()
	{
		if (_canFire)
		{
			_canFire = false;
			var isFromLeftMountain = transform.position.x < 0;
			var direction = isFromLeftMountain ? Vector3.right : Vector3.left;
			var missilePosition = isFromLeftMountain ? transform.position + _bounds.center : transform.position - _bounds.center;

			MissileAppService.CreateMissile(gameObject, missilePosition, direction, _missileVelocity);
			StartCoroutine(ReleaseFire());
		}
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

			EnemyAppService.DestroyMountainEnemy(gameObject);
		}
	}

	void MetricChanged()
	{
		Fire();
	}
}
﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SoundConfig))]
public class GroundEnemyController : MonoBehaviour {
	bool _isDead;
	int _band;
	bool _canFire = true;

	public float _dieDelay;
	public float _dieExplosionForce;

	[Range(0f, 1f)]
	public float _minAudioBandToFire;

	public float _fireInterval;
	public float _missileVelocity;
	public bool _targetSpaceship;

	void Start()
	{
		_band = GetComponent<SoundConfig>()._band;
	}

	void Update()
	{
		if (_canFire && AudioService.AudioBandBuffer[_band] >= _minAudioBandToFire)
		{
			_canFire = false;
			Vector3 direction;

			if (_targetSpaceship)
			{
				direction = SpaceshipController.Instance.transform.position - transform.position;
				direction = direction / direction.magnitude;
			}
			else
			{
				direction = transform.position.x < 0 ? Vector3.right : Vector3.left;
			}

			MissileAppService.CreateMissile(gameObject, transform.position, direction, _missileVelocity);
			StartCoroutine(ReleaseFire());
		}
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
			gameObject.GetComponent<Collider>().enabled = false;
			var rb = gameObject.AddComponent<Rigidbody>();
			rb.AddExplosionForce(_dieExplosionForce, Vector3.down, 10f);

			yield return new WaitForSeconds(_dieDelay);

			Destroy(gameObject);
		}
	}
}
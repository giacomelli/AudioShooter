﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour {

	Vector2 _direction;
	bool _canFire = true;

	public static SpaceshipController Instance { get; private set; } 

	public float _velocityMultiplier;
	public float _rotationMultiplier;
	public float _fireInterval;
	public int _lifes;
	public float _missileVelocity;

	public Spaceship Model { get; private set;}

	// Use this for initialization
	void Awake () {
		Model = new Spaceship(_lifes);
		Model.Dead += (sender, e) =>
		{
			GetComponent<MeshRenderer>().enabled = false;
		};

		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Model.IsDead)
		{
			ControlMovement();
			KeepInsideCameraView();
			ControlFire();
		}
	}

	void ControlMovement()
	{
		var direction = Vector3.zero;
	
		if (Input.GetKey(KeyCode.UpArrow))
		{
			direction += transform.right * _velocityMultiplier;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			direction -= transform.right * _velocityMultiplier;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			direction -= transform.forward * _velocityMultiplier;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			direction += transform.forward * _velocityMultiplier;
		}

		transform.position += direction;
	}

	void ControlFire()
	{
		if (_canFire)
		{
			// Front fire.
			if (Input.GetKey(KeyCode.X))
			{
				MissileAppService.CreateMissile(gameObject, transform.position, Vector3.forward, _missileVelocity);
			}
			else
			{
				// Left fire.
				if (Input.GetKey(KeyCode.Z))
				{
					MissileAppService.CreateMissile(gameObject, transform.position, Vector3.left, _missileVelocity);
				}

				// Right fire.
				if (Input.GetKey(KeyCode.C))
				{
					MissileAppService.CreateMissile(gameObject, transform.position, Vector3.right, _missileVelocity);
				}
			}

			_canFire = false;
			StartCoroutine(ReleaseFire());
		}
	}

	IEnumerator ReleaseFire()
	{
		yield return new WaitForSeconds(_fireInterval);
		_canFire = true;
	}

    void KeepInsideCameraView()
	{
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		pos.y = Mathf.Clamp01(pos.y);
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.IsEnemyMissile() || other.IsMountain() || other.IsEnemy())
		{
			Model.Hit();
		}
	}
}
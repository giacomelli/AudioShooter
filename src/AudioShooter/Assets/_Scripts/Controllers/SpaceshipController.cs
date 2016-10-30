using System;
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
	public AudioSource _fireSound;
	public AudioSource _hitSound;

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
			Vector3? direction = null;

			// Front fire.
			if (Input.GetKey(KeyCode.X))
			{
				direction = Vector3.forward;
			}
			else
			{
				// Left fire.
				if (Input.GetKey(KeyCode.Z))
				{
					direction = Vector3.left;
				}

				// Right fire.
				if (Input.GetKey(KeyCode.C))
				{
					direction = Vector3.right;
				}
			}

			if (direction.HasValue)
			{
				MissileAppService.CreateMissile(gameObject, transform.position, direction.Value, _missileVelocity);
				_fireSound.Play();
			
				_canFire = false;
				StartCoroutine(ReleaseFire());
			}
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
			_hitSound.Play();
			Model.Hit();
		}
	}
}
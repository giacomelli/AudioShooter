using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour {

	Vector2 _direction;
	bool _canFire = true;

	public static SpaceshipController Instance { get; private set; } 

	public UnityEngine.Object _missilePrefab;
	public float _velocityMultiplier;
	public float _rotationMultiplier;
	public float _fireInterval;

	public bool IsDead { get; private set; }

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsDead)
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
				MissileAppService.CreateMissile(gameObject, transform.position, Vector3.forward);
			}
			else
			{
				// Left fire.
				if (Input.GetKey(KeyCode.Z))
				{
					MissileAppService.CreateMissile(gameObject, transform.position, Vector3.left);
				}

				// Right fire.
				if (Input.GetKey(KeyCode.C))
				{
					MissileAppService.CreateMissile(gameObject, transform.position, Vector3.right);
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
			IsDead = true;
			HudController.Instance.ChangeCentralMessage("Game Over");
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
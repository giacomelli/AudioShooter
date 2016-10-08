using System.Collections;
using UnityEngine;

public class SpaceshipController : MonoBehaviour {

	Vector2 _direction;
	bool _canFire = true;

	public static SpaceshipController Instance { get; private set; } 

	public Object _missilePrefab;
	public float _velocityMultiplier;
	public float _rotationMultiplier;
	public float _fireInterval;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		ControlMovement();
		ControlFire();
	}

	void ControlMovement()
	{
		var direction = Vector3.zero;
		//var rotation = Vector3.zero;

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

		//if (Input.GetKey(KeyCode.RightArrow))
		//{
		//	rotation.y += _rotationMultiplier;
		//}
		//else if (Input.GetKey(KeyCode.LeftArrow))
		//{
		//	rotation.y -= _rotationMultiplier;
		//}

		transform.position += direction;
		//transform.Rotate(rotation);
	}

	void ControlFire()
	{
		if (_canFire)
		{
			// Left fire.
			if (Input.GetKey(KeyCode.Z))
			{
				MissileAppService.CreateMissile(gameObject, transform.position, Vector3.left);
			}

			// Front fire.
			if (Input.GetKey(KeyCode.X))
			{
				MissileAppService.CreateMissile(gameObject, transform.position, Vector3.forward);
			}

			// Right fire.
			if (Input.GetKey(KeyCode.C))
			{
				MissileAppService.CreateMissile(gameObject, transform.position, Vector3.right);
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
}




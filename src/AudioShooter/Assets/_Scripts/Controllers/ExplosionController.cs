using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	Detonator _detonator;
	public float _size;

	void Awake()
	{
		_detonator = GetComponent<Detonator>();
	}

	public void Explode(Vector3 position)
	{
		transform.position = position;
		gameObject.SetActive(true);
		_detonator.Reset();
		_detonator.size = _size;
		_detonator.Explode();
	}
}

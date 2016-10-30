using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	Detonator _detonator;
	public float _size;

	public void Explode(Vector3 position)
	{
		transform.position = position;
		gameObject.SetActive(true);

		_detonator = _detonator ?? GetComponent<Detonator>();
		_detonator.Reset();
		_detonator.size = _size;
		_detonator.Explode();
	}
}

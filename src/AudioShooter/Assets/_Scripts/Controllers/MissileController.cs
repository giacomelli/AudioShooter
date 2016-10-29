using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

	Vector3 _target;
	Vector3 _direction;
	bool _friendlyFire;
	Collider _collider;
	Renderer _renderer;

	public float _velocity;
	public float _distance;
	public float _dieDelay;

	public GameObject Shooter { get; private set; }

	public void Initialize(GameObject shooter, Vector3 direction, bool friendlyFire)
	{
		Shooter = shooter;
		_direction = direction;
		_friendlyFire = friendlyFire;
		_target = transform.position + (_direction * _distance);
		_collider.enabled = true;
		_renderer.enabled = true;
	}

	void Awake()
	{
		_collider = GetComponent<Collider>();
		_renderer = GetComponentInChildren<Renderer>();
	}

	void Update () {
		transform.position = Vector3.Lerp(transform.position, _target, _velocity);
	}

	void OnTriggerEnter(Collider other)
	{
		var allowFriendlyFire = _friendlyFire || (this.Shooter.transform.IsEnemy() ^ other.IsEnemy());

		if (allowFriendlyFire && ((other.IsEnemy() && other.gameObject != Shooter)) || other.IsMountain())
		{
			StartCoroutine(Die());
		}
	}

	IEnumerator Die()
	{
		_collider.enabled = false;
		_renderer.enabled = false;

		MissileAppService.CreateMissileExplosion(transform.position);

		yield return new WaitForSeconds(_dieDelay);

		MissileAppService.DestroyMissile(gameObject);
	}
}


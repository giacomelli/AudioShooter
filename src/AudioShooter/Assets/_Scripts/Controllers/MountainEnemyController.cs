using UnityEngine;

public class MountainEnemyController : EnemyControllerBase
{
	Bounds _bounds;

	protected override void PerformStart()
	{
		_bounds = GetComponent<MeshFilter>().mesh.bounds;
	}

	protected override void PerformFire()
	{
		var isFromLeftMountain = transform.position.x < 0;
		var direction = isFromLeftMountain ? Vector3.right : Vector3.left;
		var missilePosition = isFromLeftMountain ? transform.position + _bounds.center : transform.position - _bounds.center;

		MissileAppService.CreateMissile(gameObject, missilePosition, direction, _missileVelocity);
	}

	protected override void PerformDestroy()
	{
		EnemyAppService.DestroyMountainEnemy(gameObject);
	}
}
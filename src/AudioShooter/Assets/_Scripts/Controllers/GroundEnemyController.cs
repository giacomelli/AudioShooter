using UnityEngine;

public class GroundEnemyController : EnemyControllerBase 
{	
	public bool _targetSpaceship;

	protected override void PerformFire()
	{
		Vector3 direction;
		float metric = BehaviourMetric;

		if (_targetSpaceship)
		{
			direction = SpaceshipController.Instance.transform.position - transform.position;
			direction = direction / direction.magnitude;
		}
		else
		{
			direction = transform.position.x < 0 ? Vector3.right : Vector3.left;
		}

		MissileAppService.CreateMissile(gameObject, transform.position, direction, _missileVelocity * metric);
	}

	protected override void PerformDestroy()
	{
		EnemyAppService.DestroyGroundEnemy(gameObject);
	}
}
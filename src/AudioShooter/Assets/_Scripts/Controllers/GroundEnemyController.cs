using UnityEngine;

public class GroundEnemyController : EnemyControllerBase 
{	
	protected override void PerformFire()
	{
		MissileAppService.CreateMissileTargetingSpaceship(gameObject, _missileVelocity * BehaviourMetric);
	}

	protected override void PerformDestroy()
	{
		EnemyAppService.DestroyGroundEnemy(gameObject);
	}
}
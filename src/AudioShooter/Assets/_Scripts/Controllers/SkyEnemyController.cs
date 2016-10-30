using UnityEngine;

public class SkyEnemyController : EnemyControllerBase 
{
	protected override void PerformFire()
	{
		MissileAppService.CreateMissileTargetingSpaceship(gameObject, _missileVelocity * BehaviourMetric);
	}

	protected override void PerformDestroy()
	{
		EnemyAppService.DestroySkyEnemy(gameObject);
	}
}
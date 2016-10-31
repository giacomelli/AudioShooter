using UnityEngine;

public class SkyEnemyDeployer : EnemyDeployerBase 
{
	protected override GameObject CreateEnemy()
	{
		return EnemyAppService.CreateSkyEnemy();
	}

	protected override float GetDeployY()
	{
		return SpaceshipController.Instance.transform.position.y;
	}
}

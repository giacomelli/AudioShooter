using UnityEngine;

public class GroundEnemyDeployer : EnemyDeployerBase 
{	
	protected override GameObject CreateEnemy()
	{
		return EnemyAppService.CreateGroundEnemy();
	}
}

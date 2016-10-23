using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SoundConfig))]
public class MountainEnemyDeployer : SoundMonoBehaviour
{
	public float WaveInterval;
	public float DeployYFromSpaceship;
	[Range(0f, 1f)]
	public float MinBufferToDeploy;

	void Start()
	{
		MountainAppService.MountainDeployed += (sender, e) =>
		{
			CreateEnemyToMountain(e.GameObject);
		};
	}

	void CreateEnemyToMountain(GameObject mountain)
	{
		if (CreationMetric >= MinBufferToDeploy)
		{
			var mountainBounds = mountain.GetComponent<MeshFilter>().mesh.bounds;
			var enemy = EnemyAppService.CreateMountainEnemy();
			var enemyBounds = enemy.GetComponent<MeshFilter>().mesh.bounds;

			float mountainBoundX;
			float enemyBoundX;

			if (mountain.transform.position.x < 0)
			{
				mountainBoundX = mountainBounds.max.x;
				enemyBoundX = enemyBounds.max.x;
			}
			else
			{
				mountainBoundX = mountainBounds.min.x;
				enemyBoundX = enemyBounds.min.x;
			}

			enemy.transform.position = new Vector3(
				mountain.transform.position.x + (mountainBoundX * mountain.transform.localScale.x) + (enemyBoundX * enemy.transform.localScale.x),
				SpaceshipController.Instance.transform.position.y + DeployYFromSpaceship,
				mountain.transform.position.z);

			enemy.GetComponent<SoundConfig>()._band = Config._band;
		}
	}
}

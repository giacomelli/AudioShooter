﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SoundConfig))]
public class MountainEnemyDeployer : SoundMonoBehaviour {
	public float WaveInterval;
	public float DeployYFromSpaceship;

	void Start()
	{
		StartCoroutine(Deploy());
	}

	IEnumerator Deploy()
	{
		while (true)
		{
			CreateWave();
			yield return new WaitForSeconds(WaveInterval);
		}
	}

	void CreateWave()
	{
		var newestMountains = MountainAppService.GetNewestMountains(2);

		for (int i = 0; i < newestMountains.Length; i++)
		{
			var mountain = newestMountains[i];
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
			
			//enemy.transform.parent = mountain.transform;
			enemy.GetComponent<SoundConfig>()._band = Config._band; 
		}
	}
}

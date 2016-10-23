using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SoundConfig))]
public class SkyEnemyDeployer : SoundMonoBehaviour {
	public float _minXDeploy;
	public float _maxXDeploy;

	// TODO: move to a deployer base class.
	[Range(0f, 1f)]
	public float MinMetricToDeploy;

	void Start()
	{
		AudioAnalysisService.Instance.SoundTick += delegate {
			CreateWave();
		};
	}

	void CreateWave()
	{
		var metric = CreationMetric;
	
		if (metric >= MinMetricToDeploy)
		{
			for (int i = 0; i < 1; i++)
			{
				var enemy = EnemyAppService.CreateSkyEnemy();
				var enemyX = _minXDeploy + (_maxXDeploy - _minXDeploy) * metric;

				enemy.transform.position = new Vector3(
					enemyX,
					SpaceshipController.Instance.transform.position.y,
					AudioAnalysisService.Instance.Ticks);

				enemy.name = "Sky Enemy" + i;
				enemy.GetComponent<SoundConfig>()._band = Config._band;
			}
		}
	}
}

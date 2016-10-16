using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SoundConfig))]
public class GroundEnemyDeployer : SoundMonoBehaviour {
	public float WaveInterval;
	public float _minXDeploy;
	public float _maxXDeploy;
	int _waveNumber = 0;

	void Start()
	{
	//	StartCoroutine(Deploy());
		AudioAnalysisService.SoundTick += delegate {
			CreateWave();
		};
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
		var bandBuffer = AudioAnalysisService.AudioBandBuffer[Config._band];
		var waveSize = Convert.ToInt32(bandBuffer);

		if (bandBuffer > 0)
		{
			var newestMountains = MountainAppService.GetNewestMountains(waveSize);

			for (int i = 0; i < 1; i++)
			{
				//var mountain = newestMountains[i];
				var enemy = EnemyAppService.CreateGroundEnemy();
				var enemyX = _minXDeploy + (_maxXDeploy - _minXDeploy) * bandBuffer;

				enemy.transform.position = new Vector3(
					enemyX,
					0,
					AudioAnalysisService.Ticks);

				enemy.name = "Ground Enemy" + i;
				enemy.GetComponent<SoundConfig>()._band = Config._band;

				//Debug.LogFormat("Ground enemy position: {0}", enemy.transform.position);
			}

			_waveNumber++;
		}
	}
}

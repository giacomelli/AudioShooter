﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SoundConfig))]
public class GroundEnemyDeployer : SoundMonoBehaviour {
	public float WaveInterval;
	public float _minXDeploy;
	public float _maxXDeploy;
	public UnityEngine.Object EnemyPrefab;
	int _waveNumber = 0;

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
		var waveSize = Convert.ToInt32(AudioService.AudioBandBuffer[Config._band]);
		var newestMountains = MountainAppService.GetNewestMountains(waveSize);

		for (int i = 0; i < newestMountains.Length; i++)
		{
			var mountain = newestMountains[i];
			var enemy = (GameObject)Instantiate(EnemyPrefab);
			var enemyX = UnityEngine.Random.Range(_minXDeploy, _maxXDeploy);

			enemy.transform.position = new Vector3(
				enemyX, 
				0, 
				mountain.transform.position.z);
			
			enemy.name = "Ground Enemy" + i;
			enemy.GetComponent<SoundConfig>()._band = Config._band; 
		}

		_waveNumber++;
	}
}
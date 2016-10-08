using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MountainEnemyDeployer : MonoBehaviour {
	public int MinWaveSize;
	public int MaxWaveSize;
	public float WaveInterval;
	public UnityEngine.Object EnemyPrefab;
	List<GameObject> _wave;
	int _waveNumber = 0;

	void Start()
	{
		_wave = new List<GameObject>();
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
		var waveSize = UnityEngine.Random.Range(MinWaveSize, MaxWaveSize + 1);
		var waveBand = SoundConfig.RandomBand();
		var newestMountains = MountainAppService.GetNewestMountains(waveSize);

		for (int i = 0; i < newestMountains.Length; i++)
		{
			var mountain = newestMountains[i];
			var mountainBounds = mountain.GetComponent<MeshFilter>().mesh.bounds;
			var enemy = (GameObject)Instantiate(EnemyPrefab);
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
				SpaceshipController.Instance.transform.position.y, 
				mountain.transform.position.z);
			
			enemy.transform.parent = mountain.transform;
			enemy.name = mountain.name + "Enemy" + i;
			enemy.GetComponent<SoundConfig>()._band = waveBand; 
		
			_wave.Add(enemy);
		}

		_waveNumber++;
	}
}

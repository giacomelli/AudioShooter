using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyDeployer : MonoBehaviour {
	public int StartWaveSize;
	public float WaveInterval;
	public float WavePercentIncrease;
	public float WaveRadius;
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
		var waveSize = Convert.ToInt32(StartWaveSize + (_waveNumber * StartWaveSize) * WavePercentIncrease);
		var angle = 360f / (float)waveSize;
		var wavePivot = new GameObject("Wave" + _waveNumber);
		wavePivot.transform.position = transform.position;
		var waveBand = SoundConfig.RandomBand();

		for (int i = 0; i < waveSize; i++)
		{
			var enemy = (GameObject) Instantiate(EnemyPrefab);

			enemy.transform.position = wavePivot.transform.position;
			enemy.transform.parent = wavePivot.transform;
			enemy.name = "Enemy" + i;
			wavePivot.transform.eulerAngles = new Vector3(0, angle * i, 0);
			enemy.transform.position = Vector3.right * WaveRadius;
			enemy.GetComponent<SoundConfig>()._band = waveBand; 
		
			_wave.Add(enemy);
		}

		_waveNumber++;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScenarioDeployer : MonoBehaviour
{
	private int _deployNumber;
	public Vector3 _leftWallStart;
	public Vector3 _rightWallStart;
	public UnityEngine.Object _wallPrefab;
	public float _deployInterval;
	public Vector3 _wallScaleMultiplier;
	public int _leftWallBand;
	public int _rightWallBand;

	public static float CurrentZ { get; private set; }

	void Start()
	{
		StartCoroutine(DeployScenario());
	}

	IEnumerator DeployScenario()
	{
		while (true)
		{
			_deployNumber++;
			DeployWall(_leftWallStart, _leftWallBand);
			DeployWall(_rightWallStart, _rightWallBand);

			yield return new WaitForSeconds(_deployInterval);
		}
	}

	private void DeployWall(Vector3 wallStart, int wallBand)
	{
		CurrentZ = wallStart.z + _deployNumber;
		var wall = (GameObject)Instantiate(_wallPrefab, new Vector3(wallStart.x, wallStart.y, CurrentZ), Quaternion.identity);
		var scale = wall.transform.localScale;

		var buffer = AudioService.AudioBandBuffer[wallBand];
		wall.transform.localScale = new Vector3(buffer * _wallScaleMultiplier.x, buffer * _wallScaleMultiplier.y, buffer * _wallScaleMultiplier.z);
		wall.transform.parent = transform;
	}
}



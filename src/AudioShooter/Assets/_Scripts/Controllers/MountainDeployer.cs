using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MountainDeployer : MonoBehaviour
{
	private int _deployNumber;
	public Vector3 _leftMountainStart;
	public Vector3 _rightMountainStart;
	public float _deployInterval;
	public Vector3 _mountainScaleMultiplier;

	[Range(0, 7)]
	public int _leftMountainBand;

	[Range(0, 7)]
	public int _rightMountainBand;

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
			DeployMountain(_leftMountainStart, _leftMountainBand);
			DeployMountain(_rightMountainStart, _rightMountainBand);

			Score.Instance.RegisterMountainDeployed();
			yield return new WaitForSeconds(_deployInterval);
		}
	}

	private void DeployMountain(Vector3 mountainStart, int wallBand)
	{
		CurrentZ = mountainStart.z + _deployNumber;
		var mountain = MountainAppService.CreateMountain(new Vector3(mountainStart.x, mountainStart.y, CurrentZ));
		var scale = mountain.transform.localScale;

		var buffer = AudioService.AudioBandBuffer[wallBand];
		mountain.transform.localScale = new Vector3(buffer * _mountainScaleMultiplier.x, buffer * _mountainScaleMultiplier.y, buffer * _mountainScaleMultiplier.z);
	}
}



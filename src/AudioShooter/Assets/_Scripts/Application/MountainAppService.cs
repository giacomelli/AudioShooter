using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using Skahal.Common;

public static class MountainAppService 
{
	public static event EventHandler<MountainCreatedEventArgs> MountainCreated;
	public static event EventHandler<MountainDeployedEventArgs> MountainDeployed;

	public static GameObject CreateMountain(Vector3 position)
	{
		var mountain = SHPoolsManager.GetGameObject("Mountain", position);
		MountainCreated.Raise(typeof(MountainAppService), new MountainCreatedEventArgs(mountain));

		return mountain;
	}

	public static void MarkMountainAsDeployed(GameObject mountain)
	{
		MountainDeployed.Raise(typeof(MountainAppService), new MountainDeployedEventArgs(mountain));
	}

	public static GameObject[] GetNewestMountains(int limit)
	{
		return GameObject.FindGameObjectsWithTag("Mountain")
			             .OrderByDescending(m => m.transform.position.z)
			             .Take(limit)
			             .ToArray();
	}
}

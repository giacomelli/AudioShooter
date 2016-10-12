using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class MountainAppService 
{
	public static GameObject CreateMountain(Vector3 position)
	{
		return SHPoolsManager.GetGameObject("Mountain", position);
	}

	public static GameObject[] GetNewestMountains(int limit)
	{
		return GameObject.FindGameObjectsWithTag("Mountain")
			             .OrderByDescending(m => m.transform.position.z)
			             .Take(limit)
			             .ToArray();
	}
}

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class MountainAppService 
{
	private static Object _mountainPrefab = Resources.Load("MountainPrefab");

	public static GameObject CreateMountain(Vector3 position)
	{
		return (GameObject) GameObject.Instantiate(_mountainPrefab, position, Quaternion.identity);
	}

	public static GameObject[] GetNewestMountains(int limit)
	{
		return GameObject.FindGameObjectsWithTag("Mountain")
			             .OrderByDescending(m => m.transform.position.z)
			             .Take(limit)
			             .ToArray();
	}
}

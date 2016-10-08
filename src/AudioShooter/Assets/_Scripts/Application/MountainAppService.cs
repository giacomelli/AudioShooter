using UnityEngine;
using System.Collections;

public static class MountainAppService 
{
	private static Object _mountainPrefab = Resources.Load("MountainPrefab");

	public static GameObject CreateMountain(Vector3 position)
	{
		return (GameObject) GameObject.Instantiate(_mountainPrefab, position, Quaternion.identity);
	}
}

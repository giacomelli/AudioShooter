using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public static class EnemyAppService 
{
	public static GameObject CreateMountainEnemy()
	{
		return SHPoolsManager.GetGameObject("MountainEnemy");
	}

	public static void DestroyMountainEnemy(GameObject go)
	{
		SHPoolsManager.ReleaseGameObject("MountainEnemy", go);
	}

	public static GameObject CreateGroundEnemy()
	{
		return SHPoolsManager.GetGameObject("GroundEnemy");
	}

	public static void DestroyGroundEnemy(GameObject go)
	{
		SHPoolsManager.ReleaseGameObject("GroundEnemy", go);
	}

	public static GameObject CreateSkyEnemy()
	{
		return SHPoolsManager.GetGameObject("SkyEnemy");
	}

	public static void DestroySkyEnemy(GameObject go)
	{
		SHPoolsManager.ReleaseGameObject("SkyEnemy", go);
	}
}

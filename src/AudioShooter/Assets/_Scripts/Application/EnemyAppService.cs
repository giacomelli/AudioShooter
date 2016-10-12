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
}

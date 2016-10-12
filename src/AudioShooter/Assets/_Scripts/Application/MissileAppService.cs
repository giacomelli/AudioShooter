using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public static class MissileAppService 
{
	public static GameObject CreateMissile(GameObject shooter, Vector3 position, Vector3 direction, float velocity)
	{
		var go = (GameObject) SHPoolsManager.GetGameObject("Missile");
		go.transform.position = position;
		var controller = go.GetComponent<MissileController>();
		controller.Initialize(shooter, direction);
		controller._velocity = velocity;

		return go;
	}

	public static void DestroyMissile(GameObject gameObject)
	{
		SHPoolsManager.ReleaseGameObject("Missile", gameObject);
	}
}




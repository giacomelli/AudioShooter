using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public static class MissileAppService 
{
	private static Object _missilePrefab = Resources.Load("MissilePrefab");

	public static GameObject CreateMissile(GameObject shooter, Vector3 position, Vector3 direction)
	{
		var go = (GameObject) GameObject.Instantiate(_missilePrefab, position, Quaternion.identity);
		var controller = go.GetComponent<MissileController>();
		controller.Direction = direction;
		controller.Shooter = shooter;

		return go;
	}

}

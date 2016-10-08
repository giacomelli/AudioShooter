using UnityEngine;
using System.Collections;

public static class ColliderExtensions {

	public static bool IsEnemy(this Collider collider)
	{
		return collider.tag == "Enemy";
	}

	public static bool IsMountain(this Collider collider)
	{
		return collider.tag == "Mountain";
	}

	public static bool IsMissile(this Collider collider)
	{
		return collider.tag == "Missile";
	}

	public static bool IsMyMissile(this Collider otherCollider, MonoBehaviour me)
	{
		if (otherCollider.IsMissile())
		{
			var missileController = otherCollider.GetComponent<MissileController>();

			return missileController.Shooter == me.gameObject;
		}

		return false;
	}
}

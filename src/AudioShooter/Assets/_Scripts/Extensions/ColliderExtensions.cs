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
			return GetShooter(otherCollider) == me.gameObject;
		}

		return false;
	}

	public static bool IsEnemyMissile(this Collider otherCollider)
	{
		if (otherCollider.IsMissile())
		{
			var shooter = GetShooter(otherCollider);
				
			// If shooter is null then is an enemy (enemy was already killed).
			return shooter == null || shooter.tag == "Enemy";
		}

		return false;
	}

	public static GameObject GetShooter(this Collider otherCollider)
	{
		if (otherCollider.IsMissile())
		{
			var missileController = otherCollider.GetComponent<MissileController>();

			return missileController.Shooter;
		}

		return null;
	}
}

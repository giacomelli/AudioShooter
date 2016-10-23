using UnityEngine;
using System.Collections;

public static class ComponentExtensions {

	public static bool IsEnemy(this Component Component)
	{
		return Component.tag == "Enemy";
	}

	public static bool IsMountain(this Component Component)
	{
		return Component.tag == "Mountain";
	}

	public static bool IsMissile(this Component Component)
	{
		return Component.tag == "Missile";
	}

	public static bool IsMyMissile(this Component otherComponent, MonoBehaviour me)
	{
		if (otherComponent.IsMissile())
		{
			return GetShooter(otherComponent) == me.gameObject;
		}

		return false;
	}

	public static bool IsEnemyMissile(this Component otherComponent)
	{
		if (otherComponent.IsMissile())
		{
			var shooter = GetShooter(otherComponent);
				
			// If shooter is null then is an enemy (enemy was already killed).
			return shooter == null || shooter.tag == "Enemy";
		}

		return false;
	}

	public static GameObject GetShooter(this Component otherComponent)
	{
		if (otherComponent.IsMissile())
		{
			var missileController = otherComponent.GetComponent<MissileController>();

			return missileController.Shooter;
		}

		return null;
	}
}

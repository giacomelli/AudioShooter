using UnityEngine;

public static class MissileAppService 
{
	public static GameObject CreateMissile(GameObject shooter, Vector3 position, Vector3 direction, float velocity, bool friendlyFire = false)
	{
		var go = SHPoolsManager.GetGameObject("Missile");
		go.transform.position = position;
		var controller = go.GetComponent<MissileController>();
		controller.Initialize(shooter, direction, friendlyFire);
		controller._velocity = velocity;

		return go;
	}

	public static GameObject CreateMissileTargetingSpaceship(GameObject shooter, float velocity)
	{
		var pos = shooter.transform.position;
		var direction = SpaceshipController.Instance.transform.position - pos;
		direction = direction / direction.magnitude;

		return CreateMissile(shooter, pos, direction, velocity); 
	}

	public static void DestroyMissile(GameObject gameObject)
	{
		SHPoolsManager.ReleaseGameObject("Missile", gameObject);
	}
}
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

	public static void DestroyMissile(GameObject gameObject)
	{
		SHPoolsManager.ReleaseGameObject("Missile", gameObject);
	}

	public static GameObject CreateMissileExplosion(Vector3 position)
	{
		var go = SHPoolsManager.GetGameObject("MissileExplosion");
		go.GetComponent<ExplosionController>().Explode(position);

		return go;
	}

	public static void DestroyMissileExplosion(GameObject gameObject)
	{
		SHPoolsManager.ReleaseGameObject("MissileExplosion", gameObject);
	}
}
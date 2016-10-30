using UnityEngine;

public static class ExplosionAppService 
{	
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

	public static GameObject CreateSpaceshipExplosion(Vector3 position)
	{
		var go = (GameObject)GameObject.Instantiate(Resources.Load("SpaceshipExplosionPrefab"), position, Quaternion.identity);
		go.GetComponent<ExplosionController>().Explode(position);

		return go;
	}

	public static void DestroySpaceshipExplosion(GameObject gameObject)
	{
		throw new System.NotImplementedException();
	}
}
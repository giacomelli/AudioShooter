using UnityEngine;

[RequireComponent(typeof(SoundConfig))]
public abstract class EnemyDeployerBase : SoundMonoBehaviour {
	public float _minXDeploy;
	public float _maxXDeploy;
	[Range(0f, 1f)]
	public float MinMetricToDeploy;

	void Start()
	{
		AudioAnalysisService.Instance.SoundTick += delegate {
			CreateWave();
		};
	}

	void CreateWave()
	{
		var metric = CreationMetric;

		if (metric >= MinMetricToDeploy)
		{
			var enemy = CreateEnemy();
			var enemyX = _minXDeploy + (_maxXDeploy - _minXDeploy) * metric;

			enemy.transform.position = new Vector3(
				enemyX,
				GetDeployY(),
				AudioAnalysisService.Instance.Ticks);

			enemy.GetComponent<SoundConfig>()._band = Config._band;
		}
	}

	protected abstract GameObject CreateEnemy();

	protected virtual float GetDeployY()
	{
		return 0;
	}
}

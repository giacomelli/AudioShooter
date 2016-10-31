using UnityEngine;

[RequireComponent(typeof(SoundConfig))]
public abstract class EnemyDeployerBase : SoundMonoBehaviour {
	public float _minXDeploy;
	public float _maxXDeploy;

	[Header("The min and max beaviour metric to deploy a enemy.")]
	[Range(0f, 1f)]
	public float MinMetricToDeploy;

	[Range(0f, 1f)]
	public float MaxMetricToDeploy;

	void Start()
	{
		AudioAnalysisService.Instance.SoundTick += delegate {
			CreateWave();
		};
	}

	void CreateWave()
	{
		var metric = CreationMetric;

		if (metric >= MinMetricToDeploy && metric <= MaxMetricToDeploy)
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

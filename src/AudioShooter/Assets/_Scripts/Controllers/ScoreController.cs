using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour 
{
	public Text _scoreText;

	void Awake () 
	{
		var score = new Score();

		score.PointsUpdated += (sender, e) =>
		{
			if (!Spaceship.Instance.IsDead)
			{
				_scoreText.text = score.Points + " points";
			}
		};
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeController : MonoBehaviour 
{
	public Text _lifesText;

	void Start () 
	{
		var model = SpaceshipController.Instance.Model;
		model.LifeUpdated += (sender, e) =>
		{
			UpdateLifes();
		};

		model.Dead += (sender, e) =>
		{
			MessagesController.Instance.ChangeCentralMessage("Game Over");
		};

		UpdateLifes();
	}

	void UpdateLifes()
	{
		_lifesText.text = SpaceshipController.Instance.Model.Lifes + " lifes";
	}
}

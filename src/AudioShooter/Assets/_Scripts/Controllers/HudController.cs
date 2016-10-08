using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudController : MonoBehaviour {

	public Text _centralMessage;

	public static HudController Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}

	public void ChangeCentralMessage(string text)
	{
		_centralMessage.text = text;
	}
	                                        
}

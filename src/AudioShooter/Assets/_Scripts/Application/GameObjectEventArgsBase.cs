using UnityEngine;
using System.Collections;
using System;

public abstract class GameObjectEventArgsBase : EventArgs {

	protected GameObjectEventArgsBase(GameObject go)
	{
		GameObject = go;
	}

	public GameObject GameObject { get; private set; }
}
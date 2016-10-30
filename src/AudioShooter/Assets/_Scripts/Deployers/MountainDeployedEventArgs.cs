using UnityEngine;
using System.Collections;
using System;

public class MountainDeployedEventArgs : GameObjectEventArgsBase {

	public MountainDeployedEventArgs(GameObject mountain)
		: base(mountain)
	{
	}
}
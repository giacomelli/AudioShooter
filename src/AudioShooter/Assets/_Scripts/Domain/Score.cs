using UnityEngine;
using System.Collections;
using System;

public class Score
{
	public event EventHandler PointsUpdated;

	public Score()
	{
		Instance = this;
	}

	public static Score Instance { get; private set; }

	public int Points { get; private set; } 

	public void RegisterEnemyKilled()
	{
		Points += 1000;
		PointsUpdated(this, EventArgs.Empty);
	}

	public void RegisterMountainDeployed()
	{
		Points += 100;
		PointsUpdated(this, EventArgs.Empty);
	}
}

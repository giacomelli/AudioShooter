using UnityEngine;
using System.Collections;
using System;

public class Spaceship
{
	public event EventHandler LifeUpdated;
	public event EventHandler Dead;

	public Spaceship(int lifes)
	{
		Lifes = lifes;
		Instance = this;
	}

	public static Spaceship Instance { get; private set; }

	public int Lifes { get; private set; } 
	public bool IsDead { get; private set; }

	public void Hit()
	{
		if (Lifes > 0)
		{
			Lifes--;
			CheckLifes();
		}
	}

	void CheckLifes()
	{
		if (LifeUpdated != null)
		{
			LifeUpdated(this, EventArgs.Empty);
		}

		if (Lifes <= 0)
		{
			IsDead = true;
			Dead(this, EventArgs.Empty);
		}
	}
}

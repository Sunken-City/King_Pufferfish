//Update player health whenever an enemy collides with the bottom wall
//Restart level when player runs out of health

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;


public class HealthUIScript : MonoBehaviour
{
	public Sprite twoLeft;
	public Sprite oneLeft;

	public static bool healthDecreased = false;

	private int healthRemaining = 3;
	// Use this for initialization
	void Start()
	{
		
	}
	// Update is called once per frame
	void Update()
	{
		if(healthDecreased)
		{
			DecreaseHealth();
			healthDecreased = false;
		}
	}

	void DecreaseHealth()
	{
		healthRemaining--;

		if (healthRemaining > 1 && healthRemaining <= 2)
		{
			GetComponent<SpriteRenderer>().sprite = twoLeft;
		}
		if (healthRemaining > 0 && healthRemaining <= 1)
		{
			GetComponent<SpriteRenderer>().sprite = oneLeft;
		}
	}
}
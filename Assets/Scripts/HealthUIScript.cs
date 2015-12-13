//Update player health whenever an enemy collides with the bottom wall
//Restart level when player runs out of health

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;


public class HealthUIScript : MonoBehaviour
{
    public Sprite threeLeft;
	public Sprite twoLeft;
	public Sprite oneLeft;
    public Sprite zeroLeft;

    public static bool healthIncreased = false;
	public static bool healthDecreased = false;

	private int healthRemaining = 3;

	void Awake()
	{
        healthRemaining = 3;
        GetComponent<SpriteRenderer>().sprite = threeLeft;
        healthIncreased = false;
	    healthDecreased = false;
	}

	// Update is called once per frame
	void Update()
	{
		if(healthDecreased)
		{
			DecreaseHealth();
			healthDecreased = false;
        }
        if (healthIncreased)
        {
            IncreaseHealth();
            healthIncreased = false;
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
        if (healthRemaining <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = zeroLeft;
        }
	}

    void IncreaseHealth()
    {
        if (healthRemaining == 2)
        {
            GetComponent<SpriteRenderer>().sprite = threeLeft;
            healthRemaining++;
        }
        if (healthRemaining == 1)
        {
            GetComponent<SpriteRenderer>().sprite = twoLeft;
            healthRemaining++;
        }

    }
}
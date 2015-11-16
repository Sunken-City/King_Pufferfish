using UnityEngine;
using System.Collections;

public class Anemone : MonoBehaviour {

	public float secondsPerState = 5.0f;
	bool isActive = false;
	
	float timeSinceChange = 0.0f;
	// Use this for initialization
	void Start () 
	{
		timeSinceChange = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time - timeSinceChange > secondsPerState) 
		{
			isActive = !isActive;
			GetComponent<CircleCollider2D>().enabled = isActive;
			timeSinceChange = Time.time;
			GetComponent<SpriteRenderer>().color = isActive ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(0.4f, 0.4f, 0.4f, 0.5f);
		}
	}
}

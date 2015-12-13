using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FloatAndWiggleUp : MonoBehaviour 
{

    public float riseSpeed = 1.0f;
    public float wiggleAmplitude = 0.3f;
    public float wiggleSpeedMin = 0.5f;
    public float wiggleSpeedMax = 3.0f;
    
    float wiggleSpeed = 1.0f;
    float spawnTime;

	// Use this for initialization
	void Start () 
    {
        spawnTime = Time.time;
        wiggleSpeed = Random.Range(wiggleSpeedMin, wiggleSpeedMax);
	}
	
	// Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sin(wiggleSpeed * (Time.time + spawnTime)) * wiggleAmplitude, riseSpeed);
	}
}

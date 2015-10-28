using UnityEngine;
using System.Collections;

public class UnderwaterMine : MonoBehaviour {

	private float spawnTime;

	private GameObject[] Enemies;
	
	// Use this for initialization
	void Start () 
	{
		spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sin(Time.time + spawnTime));
	}

	void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
	{
		if (WhoCollidedWithMe.tag == "Bullet")
		{
			//Spawn a particle on dead enemy
			//GameObject inkParticle = (GameObject)GameObject.Instantiate(DeathInkParticle, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

			//GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(20f, 40f)) * 30);
			//Destroy bullet and enemy
			Destroy(WhoCollidedWithMe.gameObject);

			if(Enemies == null)
			{
				Enemies = GameObject.FindGameObjectsWithTag("Enemy");
			}

			foreach(GameObject enemy in Enemies)
			{
				Destroy(enemy);
			}

			DestroyObject(gameObject);
		}
	}
}
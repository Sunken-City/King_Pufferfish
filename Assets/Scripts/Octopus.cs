using UnityEngine;
using System.Collections;

public class Octopus : MonoBehaviour {
    
    public GameObject DeathParticle;

	// Use this for initialization
	void Start () 
	{	
	}
	
	// Update is called once per frame
    void Update()
    {
		GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos (Time.time), GetComponent<Rigidbody2D>().velocity.y);
    }

	void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        if (WhoCollidedWithMe.tag == "Bullet")
        {
            GameObject thisParticle = (GameObject)GameObject.Instantiate(DeathParticle);
            thisParticle.transform.position = new Vector3 (transform.position.x, transform.position.y, -9);   
            Destroy(WhoCollidedWithMe.gameObject);
            DestroyObject(gameObject);
        }
    }
}
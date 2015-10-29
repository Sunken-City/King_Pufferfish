//Update player health whenever an enemy collides with the bottom wall
//Restart level when player runs out of health

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthWall : MonoBehaviour
{

    //Variables
    public static int health;

    private string HealthText;

    public Sprite wallOneHit;
    public Sprite wallTwoHits;

    // Use this for initialization
    void Start()
    {
        //Set initial health
        health = 3;
    }
    // Update is called once per frame
    void Update()
    {
        if(health > 1 && health <= 2)
        {
            GetComponent<SpriteRenderer>().sprite = wallOneHit;
        }
        if(health > 0 && health <= 1)
        {
            GetComponent<SpriteRenderer>().sprite = wallTwoHits;
        }
        if (health <= 0)
        {
            //reload level on death
            Application.LoadLevel("Game_Over_Scene");
        }
    }

    //Update Health if hit a enemies collide with wall
    void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        //Check for collision and reduce health
        if (WhoCollidedWithMe.tag == "Enemy")
        {
            health --;
            
            //Destroy enemy upon collision
            Destroy(WhoCollidedWithMe.gameObject);
        }

    }
}
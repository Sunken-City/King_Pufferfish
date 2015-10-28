//Update player health whenever an enemy collides with the bottom wall
//Restart level when player runs out of health

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthWall : MonoBehaviour
{

    //Variables
    public int health;
    private string HealthText;

    // Use this for initialization
    void Start()
    {
        //Set initial health
        health = 100;
    }
    // Update is called once per frame
    void Update()
    {
        //Update Score UI
        //GameObject.Find("HealthText").GetComponent<Text>().text = health.ToString();
    }

    //Update Health if hit a enemies collide with wall
    void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        //Check for collision and reduce health
        if (WhoCollidedWithMe.tag == "Enemy")
        {
            health -= 25;
            if (health <= 0)
            {
                //Reload level on Death
                Application.LoadLevel(Application.loadedLevel);
            }
            //Destroy enemy upon collision
            Destroy(WhoCollidedWithMe.gameObject);
        }
    }
}

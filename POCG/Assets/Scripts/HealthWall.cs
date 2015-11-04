//Update player health whenever an enemy collides with the bottom wall
//Restart level when player runs out of health

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;


public class HealthWall : MonoBehaviour
{
	public AudioClip WallHurtSound;
    public int health;
	public static bool hitByMine = false;
    public float screenShakeMagnitude = 0.50f;
    public Sprite wallOneHit;
    public Sprite wallTwoHits;
	public float firstHitOffset;
	public float secondHitOffset;

    private string HealthText;
    // Use this for initialization
    void Start()
    {
        //Set initial health
        health = 3;
    }
    // Update is called once per frame
    void Update()
    {
		if (hitByMine) {
			TakeDamage();
			hitByMine = false;
		}
    }

	public void TakeDamage()
	{            
		health --;
		GameController.instance.ShakeScreen(screenShakeMagnitude + (0.1f *(1 - (health / 3.0f))));
		GameController.instance.PlaySound(WallHurtSound);
		if (health > 1 && health <= 2) {
			GetComponent<SpriteRenderer> ().sprite = wallOneHit;
			GetComponent<BoxCollider2D> ().offset = new Vector2 (GetComponent<BoxCollider2D> ().offset.x, GetComponent<BoxCollider2D> ().offset.y - firstHitOffset);
		}
		if (health > 0 && health <= 1) {
			GetComponent<SpriteRenderer> ().sprite = wallTwoHits;
			GetComponent<BoxCollider2D> ().offset = new Vector2 (GetComponent<BoxCollider2D> ().offset.x, GetComponent<BoxCollider2D> ().offset.y - secondHitOffset);
		}
		if (health <= 0) {
			//reload level on death
			Application.LoadLevel ("Game_Over_Scene");
		}
	}

    //Update Health if hit a enemies collide with wall
    void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        //Check for collision and reduce health
        if (WhoCollidedWithMe.tag == "Enemy")
        {
			TakeDamage();
			//Destroy enemy upon collision
			Destroy(WhoCollidedWithMe.gameObject);
        }

    }
}
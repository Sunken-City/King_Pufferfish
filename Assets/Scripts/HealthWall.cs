//Update player health whenever an enemy collides with the bottom wall
//Restart level when player runs out of health

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;


public class HealthWall : MonoBehaviour
{
	public AudioClip[] WallHurtSound;
    public int health;
	public static bool hitByMine = false;
    public static bool clearedRageWave = false;
    public float screenShakeMagnitude = 0.50f;
    public Sprite wallNoHit;
    public Sprite wallOneHit;
    public Sprite wallTwoHits;
	public float firstHitOffset;
	public float secondHitOffset;
    public ParticleSystem WallDamagePartcle;
    public GameObject fadeToBlackground;

    // Use this for initialization
    void Start()
    {
        //Set initial health
        health = 3;
        WallDamagePartcle.Stop();
    }
    // Update is called once per frame
    void Update()
    {
		if (hitByMine) {
			TakeDamage();
			hitByMine = false;
		}
        if (clearedRageWave)
        {
            HealFromRage();
            clearedRageWave = false;
        }
    }

    public void HealFromRage()
    {
        if (health == 2)
        {
            GetComponent<SpriteRenderer>().sprite = wallNoHit;
            GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, GetComponent<BoxCollider2D>().offset.y + firstHitOffset);
            health++;
        }
        if (health == 1)
        {
            GetComponent<SpriteRenderer>().sprite = wallOneHit;
            GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, GetComponent<BoxCollider2D>().offset.y + secondHitOffset);
            health++;
        }
    }

	public void TakeDamage()
	{            
		health --;
		GameController.instance.ShakeScreen(screenShakeMagnitude + (0.1f *(1 - (health / 3.0f))));
		GameController.instance.PlayRandomSound(WallHurtSound);
        WallDamagePartcle.Stop();
        WallDamagePartcle.Play();
		if (health > 1 && health <= 2) {
			GetComponent<SpriteRenderer> ().sprite = wallOneHit;
			GetComponent<BoxCollider2D> ().offset = new Vector2 (GetComponent<BoxCollider2D> ().offset.x, GetComponent<BoxCollider2D> ().offset.y - firstHitOffset);
		}
		if (health > 0 && health <= 1) {
			GetComponent<SpriteRenderer> ().sprite = wallTwoHits;
			GetComponent<BoxCollider2D> ().offset = new Vector2 (GetComponent<BoxCollider2D> ().offset.x, GetComponent<BoxCollider2D> ().offset.y - secondHitOffset);
		}
		if (health <= 0) 
        {
            StartCoroutine("BeginApocalypse");
            this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
		}

        HealthUIScript.healthDecreased = true;
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

    IEnumerator BeginApocalypse()
    {
        GameObject fadeBlackground = Instantiate(fadeToBlackground) as GameObject;
        float fadeAmount = 0.0f;
        bool levelNotLoading = true;
        GameController.instance.LockFiringForSeconds(100.0f);
        while (true)
        {
            fadeBlackground.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, fadeAmount);
            fadeAmount += 0.01f;
            float inverseFade = 1.0f - fadeAmount;
            GameController.instance.musicPlayerSource.pitch = inverseFade > 0.0f ? inverseFade : 0.0f;
            yield return new WaitForSeconds(0.01f);
            if (fadeAmount >= 1.0f && levelNotLoading)
            {
                Application.LoadLevelAsync("GameOverScene");
                levelNotLoading = false;
            }
        }
    }
}
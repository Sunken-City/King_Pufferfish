using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    //Public Variables
    public float bulletSpeed = 1.0f;
    public GameObject Bullet;
    public float bulletCooldownSeconds = 0.05f;
    public AudioClip BulletSound;
    public float screenShakeMagnitude = 0.05f;
    public float bulletFireRateMultiplier = 1.0f;

    //Private variables
    private Vector3 target;
    private float timerStart;
    private Vector2 bulletDirection;

    void Start()
    {
        //Start timer
        timerStart = Time.time;
    }

    private void FireBullet()
    {
        //Save mouse location on click
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Find player position
        GameObject player = GameObject.Find("Player");
        Vector3 playerPosition = player.transform.position;

        //Calculate player bounds
        float playerLeft = playerPosition.x - (player.GetComponent<CircleCollider2D>().radius * player.transform.localScale.x);
        float playerRight = playerPosition.x + (player.GetComponent<CircleCollider2D>().radius * player.transform.localScale.x);
        float playerTop = playerPosition.y + (player.GetComponent<CircleCollider2D>().radius * player.transform.localScale.y);

        if((target.y > playerTop && (target.x < playerLeft || target.x > playerRight)) || target.y > playerTop)
        {
            //Spawn bullet and limit lifespan
            GameController.instance.PlaySound(BulletSound);
            GameObject thisBullet = (GameObject)GameObject.Instantiate(Bullet);
            Destroy(thisBullet, 3.0f);

            //Move new bullet to player position
            thisBullet.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 1);

            //Calculate vector between mouse click and bullet position
            Vector2 directionNormalized = ((Vector2)(target - thisBullet.transform.position)).normalized;
            bulletDirection = directionNormalized * bulletSpeed;

            //Apply velocity in mouse direction
            thisBullet.GetComponent<Rigidbody2D>().velocity = (bulletDirection);

            //Rotate bullet towards its direction
            thisBullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, bulletDirection);

            //Reset timer to current
            timerStart = Time.time;

            //Make player look at mouse
            player.transform.rotation = Quaternion.FromToRotation(Vector3.up, -bulletDirection);

            GameController.instance.ShakeScreen(screenShakeMagnitude);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Register mouse tap or touch. Shoot if cooldown is over
        if (GameController.instance.rageMode && Input.GetKey(KeyCode.Mouse0) && (Time.time >= timerStart + (bulletCooldownSeconds / bulletFireRateMultiplier)))
        {
            FireBullet();            
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && !GameController.instance.scrubLock && (Time.time >= timerStart + bulletCooldownSeconds))
        {
            FireBullet();
        }
    }
}
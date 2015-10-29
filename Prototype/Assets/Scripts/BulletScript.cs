using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    //Public Variables
    public float bulletSpeed = 1.0f;
    public GameObject Bullet;
    //Private variables
    private Vector3 target;
    private float timerStart;
    private Vector2 bulletDirection;

    void Start()
    {
        //Start timer
        timerStart = Time.time;
    }
    // Update is called once per frame
    void OnMouseDown()
    {
        //Register mouse tap or touch. Shoot if cooldown is over
        if (Input.GetKeyDown(KeyCode.Mouse0) && (Time.time >= timerStart + 0.2f))
        {
            //Spawn bullet and limit lifespan
            GameObject thisBullet = (GameObject)GameObject.Instantiate(Bullet);
            Destroy(thisBullet, 3.0f);
            //Move new bullet to player position
            thisBullet.transform.position = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, thisBullet.transform.position.z);
            //Save mouse location on click
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Calculate vector between mouse click and bullet position
            bulletDirection = (new Vector2(((Vector2)(target - thisBullet.transform.position)).normalized.x, ((Vector2)(target - thisBullet.transform.position)).normalized.y) * bulletSpeed);
            //Apply velocity in mouse direction
            thisBullet.GetComponent<Rigidbody2D>().velocity = (bulletDirection);
            //Rotate bullet towards its direction
            thisBullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, bulletDirection);
            //Reset timer to current
            timerStart = Time.time;
        }
        //Make player look at mouse
        transform.rotation = Quaternion.FromToRotation(Vector3.up, bulletDirection);
    }
}
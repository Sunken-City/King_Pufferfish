using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    public float bulletSpeed = 1.0f;
    public GameObject Bullet;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {



            GameObject thisBullet = (GameObject)GameObject.Instantiate(Bullet);
            thisBullet.transform.position = GameObject.Find("Player").transform.position;

            Vector3 ViewmousePos = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
            Vector3 WorldmousePos = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportToWorldPoint(ViewmousePos);
            // thisBullet.transform.LookAt(WorldmousePos.normalized);
            Vector2 bulletDirection = (new Vector2(((Vector2)(WorldmousePos - thisBullet.transform.position)).normalized.x, ((Vector2)(WorldmousePos - thisBullet.transform.position)).normalized.y) * bulletSpeed);
            thisBullet.GetComponent<Rigidbody2D>().velocity = (bulletDirection);
            //thisBullet.GetComponent<Rigidbody2D>().AddForce(thisBullet.transform.forward * bulletSpeed);



        }

    }
}
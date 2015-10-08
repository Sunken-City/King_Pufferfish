using UnityEngine;
using System.Collections;

public class SprayBulletScript : MonoBehaviour
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

			Vector2 DirectionToMouse = (Vector2)WorldmousePos.normalized;
			float angle = (Mathf.Atan2(DirectionToMouse.y, DirectionToMouse.x) * Mathf.Rad2Deg) - 90.0f;
			thisBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector2 bulletDirection = (new Vector2(((Vector2)(WorldmousePos - thisBullet.transform.position)).normalized.x, ((Vector2)(WorldmousePos - thisBullet.transform.position)).normalized.y) * bulletSpeed);
            thisBullet.GetComponent<Rigidbody2D>().velocity = (bulletDirection);
        }

    }
}
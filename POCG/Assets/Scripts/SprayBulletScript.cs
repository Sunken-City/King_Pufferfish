using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SprayBulletScript : MonoBehaviour
{

	public float bulletSpeed = 1.0f;
	public GameObject Bullet;
	public float chargeMeterMax = 1.0f;
	public AudioClip BulletSpraySound;
	public float rageDurationSeconds = 5.0f;

	private bool mouseOver = false;
	private GameObject[] AllBullets;
	static float chargeMeter = 0.0f;

	public int numberBullets = 7;


	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void OnMouseDown()
	{
		float angleIncrement = 180.0f / ((float)numberBullets - 1.0f);

		if (chargeMeter >= chargeMeterMax)
		{
			Vector3 playerPosition = GameObject.Find("Player").transform.position;

			for (float angle = 0; angle <= 180; angle += angleIncrement)
			{
				GameObject thisBullet = (GameObject)GameObject.Instantiate(Bullet);
				Destroy(thisBullet, 3.0f);
				thisBullet.transform.position = playerPosition;
				thisBullet.transform.rotation = Quaternion.AngleAxis(angle - 90.0f, new Vector3(0.0f, 0.0f, 1.0f));

				Vector3 bulletDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f);
				Vector3 bulletVelocity = bulletDirection * bulletSpeed;

				thisBullet.GetComponent<Rigidbody2D>().velocity = (Vector2)(bulletVelocity);
				thisBullet.transform.position += bulletVelocity.normalized;
				GameController.instance.PlaySound(BulletSpraySound);
			}
			GameController.instance.RageForSeconds(rageDurationSeconds);
			chargeMeter = 0.0f;
		}
	}

	public static void ChargeUp(float swipeStrength)
	{
		chargeMeter += swipeStrength / 10.0f;
		Debug.Log("charge = " + chargeMeter);
	}

	public static float GetChargeMeter()
	{
		return chargeMeter;
	}

	public void OnDestroy()
	{
		chargeMeter = 0.0f;
	}
}
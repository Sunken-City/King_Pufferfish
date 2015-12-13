//Destroy bullet and enemy on collision, and spawn a particle on enemy location
using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour
{
    public GameObject HeartGainedParticle;
    public GameObject BubbleReference;
    public int HP = 1;
    public AudioClip popSound;

    private float screenLeftBounds = -3.124f;
    private float screenRightBounds = 3.134f;
    private float spawnTime;

    private bool isDead = false;
    private float maxHP;

    // Use this for initialization
    void Start()
    {
        spawnTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sin(Time.time + spawnTime));
        BubbleReference.transform.localScale = new Vector2((Mathf.Sin(Time.time) / 2.0f) + 3.5f, (Mathf.Sin(Time.time + 1.0f) / 2.0f) + 3.5f);
    }

    private void Flee()
    {
        GameController.instance.IncrementMultiplier();
        //Spawn a particle on dead enemy
        GameObject particle = GameObject.Instantiate(HeartGainedParticle, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity) as GameObject;
        Destroy(particle, 1.0f);
        HealthWall.clearedRageWave = true;
        HealthUIScript.healthIncreased = true;
        isDead = true;
        //Destroy bullet and enemy
        DestroyObject(gameObject);
        GameController.instance.PlaySound(popSound);
    }

    void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        if (WhoCollidedWithMe.tag == "Bullet")
        {
            Destroy(WhoCollidedWithMe.gameObject);
            HP--;
            float hpRatio = HP / maxHP;
            if (HP <= 0)
            {
                Flee();
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, hpRatio, hpRatio, 1.0f);
            }
        }
    }
}
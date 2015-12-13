//Destroy bullet and enemy on collision, and spawn a particle on enemy location
using UnityEngine;
using System.Collections;

public class GiantEnemy : MonoBehaviour
{
    public GameObject firstSplitEnemy;
    public GameObject secondSplitEnemy;
    public int HP = 1;
    public int scoreForDefeating = 1;
    public float speedMultiplierForChildren = 1.0f;

    private float screenLeftBounds = -3.124f;
    private float screenRightBounds = 3.134f;

    private bool isDead = false;
    private float maxHP;

    // Use this for initialization
    void Start()
    {
        maxHP = HP;
    }
    // Update is called once per frame
    void Update()
    {
        float leftBoundsX = screenLeftBounds + (GetComponent<CircleCollider2D>().radius * transform.localScale.x);
        float rightBoundsX = screenRightBounds - (GetComponent<CircleCollider2D>().radius * transform.localScale.x);

        if (transform.position.x < leftBoundsX)
        {
            transform.position = new Vector3(leftBoundsX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightBoundsX)
        {
            transform.position = new Vector3(rightBoundsX, transform.position.y, transform.position.z);
        }
    }

    private void SplitIntoTwo()
    {
        int scoreToAdd = (int)((float)scoreForDefeating * GameController.instance.scoreMultiplier);
        GameController.instance.AddScore(scoreToAdd, transform.position);
        GameController.instance.IncrementMultiplier();
        Vector3 firstSplitEnemyPosition = new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z);
        Vector3 secondSplitEnemyPosition = new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z);

        GameObject baby1 = GameObject.Instantiate(firstSplitEnemy, firstSplitEnemyPosition, Quaternion.Euler(0.0f, 0.0f, 180.0f)) as GameObject;
        GameObject baby2 = GameObject.Instantiate(secondSplitEnemy, secondSplitEnemyPosition, Quaternion.Euler(0.0f, 0.0f, 180.0f)) as GameObject;

        Vector2 parentVelocity = this.GetComponent<Rigidbody2D>().velocity;
        baby1.GetComponent<Rigidbody2D>().velocity = parentVelocity * speedMultiplierForChildren;
        baby2.GetComponent<Rigidbody2D>().velocity = parentVelocity * speedMultiplierForChildren;

        isDead = true;
        DestroyObject(gameObject);
    }

    void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        if(!isDead && WhoCollidedWithMe.tag == "Bullet")
        {
            Destroy(WhoCollidedWithMe.gameObject);
            HP--;
            float hpRatio = HP / maxHP;
            if (HP <= 0)
            {
                SplitIntoTwo();
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, hpRatio, hpRatio, 1.0f);
            }
        }
    }
}
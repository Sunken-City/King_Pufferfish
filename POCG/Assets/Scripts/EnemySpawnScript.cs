using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour {

    public float SpawnDelay = 1.0f;
    public float EnemySpeed = -1.0f;
    public GameObject EnemySpawner;
    public GameObject Enemy1;
    private GameObject Player;
   
    public int rand = 1;
    float timerStart;

    // Use this for initialization
    void Start()
    {
        timerStart = Time.time;
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= timerStart + rand)
        {
            rand = Random.Range(1, 2);
            int randXSpawn = Random.Range(-3, 4);
            GameObject thisEnemy = (GameObject)GameObject.Instantiate(Enemy1);
            thisEnemy.transform.position = GameObject.Find("EnemySpawner").transform.position;
          //  GameObject thisPlayer = GameObject.Find("Player");



        //    Vector2 enemyDirection = (new Vector2(((Vector2)(thisPlayer.transform.position + thisEnemy.transform.position)).normalized.x, ((Vector2)(thisPlayer.transform.position + thisEnemy.transform.position)).normalized.y) * EnemySpeed);
          //  thisEnemy.GetComponent<Rigidbody2D>().velocity = (enemyDirection);
            //thisEnemy.transform.position = new Vector2(randXSpawn, thisEnemy.transform.position.y);


            //Vector3 rotDir = thisPlayer.transform.position - thisEnemy.transform.position;

            //thisEnemy.transform =  Vector3.RotateTowards(transform.forward, rotDir, 90, 2.0f)
           

          
            
            // thisEnemy.transform.position = GameObject.Find("EnemySpawner").transform.position;
          
            
            //thisEnemy.transform.position = Vector2.MoveTowards(new Vector2 (thisEnemy.transform.position.x, thisEnemy.transform.position.y), thisPlayer.transform.position, 100.0f * Time.deltaTime);
          
            
             thisEnemy.transform.position = new Vector2(randXSpawn, thisEnemy.transform.position.y);

          
           

            thisEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, EnemySpeed);
            //thisPipe.GetComponent<pipeScript>().Go();

            timerStart = Time.time;


        }

    }
}

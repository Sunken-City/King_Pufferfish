using UnityEngine;
using System.Collections;

public class PipeSpawnerScript : MonoBehaviour {


    public float SpawnDelay = 1.0f;
    public float pipeSpeed = -1.0f;
    public GameObject SpawnPoint;
    public GameObject PipePrefab;
    public int rand = 2;
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
            rand = Random.Range(2, 4);
            int randPipeHeight = Random.Range(-2, 6);
            GameObject thisPipe = (GameObject)GameObject.Instantiate(PipePrefab);
           
            thisPipe.transform.position = GameObject.Find("PipeSpawnPoint").transform.position;
            thisPipe.transform.position = new Vector2(thisPipe.transform.position.x, randPipeHeight);
            thisPipe.GetComponent<Rigidbody2D>().velocity = new Vector2(pipeSpeed, 0.0f);
            //thisPipe.GetComponent<pipeScript>().Go();

            timerStart = Time.time;


        }

    }
}

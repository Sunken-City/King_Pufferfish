using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Wave[] waves;
    public float defaultEnemySpeed = 1.0f;
    public float speedConstant = 1.0f;
    public int score = 0;

    private float timeSinceLastSpawn = 0.0f;
    private float timeUntilNextSpawn = 2.0f;
    private int currentWaveIndex = 0;
    private int currentEncounterIndex = 0;
    private bool currentWaveFinished = false;

    void Awake()
    {
        //If we have a second instance, destroy it first
        if (instance)
        {
            Debug.Log("Destroying irrelevant GameController instance");
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        timeSinceLastSpawn = 0.0f;
        Debug.Log("Starting wave #" + currentWaveIndex);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (currentWaveIndex < waves.Length)
        {
            if (timeUntilNextSpawn <= timeSinceLastSpawn)
            {
                SpawnFromWave();
            }
            if (currentWaveFinished)
            {
                currentWaveIndex++;
                Debug.Log("Starting wave #" + currentWaveIndex);
                currentEncounterIndex = 0;
                currentWaveFinished = false;
            }
        }
        
    }

    public void AddScore(int scoreIncrement)
    {
        score += scoreIncrement;
        Debug.Log("score is now :" + score);
    }

    private void SpawnFromWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        if (currentEncounterIndex < currentWave.EncounterList.Length)
        {
            Debug.Log("Spawning Encounter #" + currentEncounterIndex);
            Encounter currentEncounter = currentWave.EncounterList[currentEncounterIndex];
            GameObject enemy = Instantiate(currentEncounter.enemy, new Vector3(currentEncounter.spawnPositionX, 14.0f, 0.0f), Quaternion.identity) as GameObject;
            
            float enemySpeed = currentEncounter.speed * speedConstant;
            if (enemySpeed == 0.0f)
	        {
		        enemySpeed = defaultEnemySpeed * speedConstant;
	        }
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -enemySpeed);

            timeSinceLastSpawn = 0.0f;
            timeUntilNextSpawn = currentEncounter.waitSecondsAfterSpawn;
            currentEncounterIndex++;
        }
        else
        {
            currentWaveFinished = true;
            Debug.Log("Wave Finished");
        }
    }

}

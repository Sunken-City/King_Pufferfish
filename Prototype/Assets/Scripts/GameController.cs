using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Wave[] waves;
    public float defaultEnemySpeed = 1.0f;
    public float speedConstant = 1.0f;
    public int score = 0;
    public AudioClip ambientSound;
    public GameObject soundPlayer;

    private float timeSinceLastSpawn = 0.0f;
    private float timeUntilNextSpawn = 2.0f;
    private int currentWaveIndex = 0;
    private int currentEncounterIndex = 0;
    private bool currentWaveFinished = false;
    private GameObject ambientPlayer;

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

    void OnDestroy()
    {
        Destroy(ambientPlayer);
    }

    // Use this for initialization
    void Start()
    {
        timeSinceLastSpawn = 0.0f;
        Debug.Log("Starting wave #" + currentWaveIndex);
        ambientPlayer = Instantiate(soundPlayer, Vector3.zero, Quaternion.identity) as GameObject;
        AudioSource ambientSoundPlayer = ambientPlayer.GetComponent<AudioSource>();
        ambientSoundPlayer.clip = ambientSound;
        ambientSoundPlayer.loop = true;
        ambientSoundPlayer.Play();
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

            //Calculate speed constant
            float enemySpeed = currentEncounter.speed * speedConstant;
            if (enemySpeed == 0.0f)
            {
                enemySpeed = defaultEnemySpeed * speedConstant;
            }

            //Calculate the spawn position and velocity based on where the enemy is going to move.
            Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
            Vector2 enemyVelocity = new Vector2(0.0f, 0.0f);
            if (currentEncounter.movementDirection == MovementDirection.DOWN)
            {
                spawnPosition = new Vector3(currentEncounter.spawnPositionOffset, 14.0f, 0.0f);
                enemyVelocity = new Vector2(0.0f, -enemySpeed);
            }
            else if (currentEncounter.movementDirection == MovementDirection.LEFT_TO_RIGHT)
            {
                spawnPosition = new Vector3(-4.0f, currentEncounter.spawnPositionOffset + 8.0f, 0.0f);
                enemyVelocity = new Vector2(enemySpeed, 0.0f);
            }
            else if (currentEncounter.movementDirection == MovementDirection.RIGHT_TO_LEFT)
            {
                spawnPosition = new Vector3(4.0f, currentEncounter.spawnPositionOffset + 8.0f, 0.0f);
                enemyVelocity = new Vector2(-enemySpeed, 0.0f);
            }

            if (!currentEncounter.enemy)
            {
                currentWaveFinished = true;
                Debug.LogWarning("Current wave cut short, missing a gameobject for element " + currentEncounterIndex);
                return;
            }

            GameObject enemy = Instantiate(currentEncounter.enemy, spawnPosition, Quaternion.identity) as GameObject;

            if (enemy.GetComponent<Squid>())
            {
                enemy.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
            }
          
            enemy.GetComponent<Rigidbody2D>().velocity = enemyVelocity;

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

    public void PlaySound(AudioClip sound)
    {
        StartCoroutine(PlaySoundAsync(sound));
    }

    public IEnumerator PlaySoundAsync(AudioClip sound)
    {
        GameObject tempSoundPlayer = Instantiate(soundPlayer, Vector3.zero, Quaternion.identity) as GameObject;
        AudioSource player = tempSoundPlayer.GetComponent<AudioSource>();
        player.clip = sound;
        float timeToPlay = player.clip.length;
        player.Play();
        yield return new WaitForSeconds(timeToPlay);
        Destroy(tempSoundPlayer);
    }

}

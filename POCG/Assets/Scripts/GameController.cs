using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Wave[] waves;
    public float defaultEnemySpeed = 1.0f;
    public float difficultySpeedMultiplier = 1.0f;
    public float difficultySpeedIncrementPerWave = 0.02f;
    public float difficultySpeedMultiplierCap = 1.5f;
    public float rageSpawnMultiplier = 2.0f;
    public float rageSpeedMultiplier = 1.5f;
    public AudioClip ambientSound;
    public GameObject soundPlayer;
    public int numberOfBeginningWavesToRemove = 2;

    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public bool scrubLock;
    [HideInInspector]
    public bool rageMode = false;
    [HideInInspector]
    public float rageDuration = 0.0f;

    private float timeSinceLastSpawn = 0.0f;
    private float timeUntilNextSpawn = 2.0f;
    private int currentWaveIndex = 0;
    private int currentEncounterIndex = 0;
    private bool currentWaveFinished = false;
    private GameObject ambientPlayer;
    private float screenShakeMagnitude = 0.0f;
    private Vector3 defaultCameraPosition;

    void Awake()
    {
        //If we have a second instance, destroy it first
        if (instance)
        {
            Debug.Log("Destroying irrelevant GameController instance");
            Destroy(instance.gameObject);
        }

        instance = this;
        defaultCameraPosition = Camera.main.transform.position;
    }

    void OnDestroy()
    {
        Destroy(ambientPlayer);
        StopAllCoroutines();
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
        if (Input.GetMouseButton(1))
        {
           // RageForSeconds(3.0f);
        }
        float adjustedTimeSinceFrame = Time.deltaTime * (rageMode ? rageSpawnMultiplier : 1.0f);
        timeSinceLastSpawn += adjustedTimeSinceFrame;
        if (currentWaveIndex < waves.Length)
        {
            if (timeUntilNextSpawn <= timeSinceLastSpawn)
            {
                SpawnFromWave();
            }
            if (currentWaveFinished)
            {
                currentWaveIndex++;
                difficultySpeedMultiplier += difficultySpeedIncrementPerWave;
                if (difficultySpeedMultiplier > difficultySpeedMultiplierCap)
                {
                    difficultySpeedMultiplier = difficultySpeedMultiplierCap;
                }
                currentEncounterIndex = 0;
                currentWaveFinished = false;
            }
        }
        else
        {
            Debug.Log("Out of waves, randomizing!");
            ShuffleWaves(waves);
            numberOfBeginningWavesToRemove = 0;
            currentWaveIndex = 0;
        }
        Vector3 cameraOffset = new Vector3(Random.Range(0.0f, screenShakeMagnitude), Random.Range(0.0f, screenShakeMagnitude), 0.0f);
        Camera.main.transform.position = defaultCameraPosition + cameraOffset;

        screenShakeMagnitude -= Time.deltaTime;
        if (screenShakeMagnitude < 0.0f)
        {
            screenShakeMagnitude = 0.0f;
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
            float enemySpeed = currentEncounter.speed;
            if (enemySpeed == 0.0f)
            {
                enemySpeed = defaultEnemySpeed;
            }
            enemySpeed *= difficultySpeedMultiplier;
            enemySpeed *= rageMode ? rageSpeedMultiplier : 1.0f;

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
                spawnPosition = new Vector3(-4.5f, currentEncounter.spawnPositionOffset + 8.0f, 0.0f);
                enemyVelocity = new Vector2(enemySpeed, 0.0f);
            }
            else if (currentEncounter.movementDirection == MovementDirection.RIGHT_TO_LEFT)
            {
                spawnPosition = new Vector3(4.5f, currentEncounter.spawnPositionOffset + 8.0f, 0.0f);
                enemyVelocity = new Vector2(-enemySpeed, 0.0f);
            }

            if (!currentEncounter.enemy)
            {
                currentWaveFinished = true;
                Debug.LogWarning("Current wave cut short, missing a gameobject for element " + currentEncounterIndex);
                return;
            }

            GameObject enemy = Instantiate(currentEncounter.enemy, spawnPosition, Quaternion.identity) as GameObject;
            enemy.GetComponent<Rigidbody2D>().velocity = enemyVelocity;

            if (enemy.GetComponent<Squid>())
            {
                enemy.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
            }
            else if (rageMode && enemy.GetComponent<UnderwaterMine>())
            {
                Destroy(enemy);
                Debug.Log("Mine prevented from spawning because of rage mode");
            }
          
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
        float timeToPlay = 0.0f;
        if (sound != null)
        {
            timeToPlay = player.clip.length;
            player.Play();
        }
        else
        {
            Debug.LogWarning("Sound was not attached for a PlaySound call");
        }
        yield return new WaitForSeconds(timeToPlay);
        Destroy(tempSoundPlayer);
    }

    //Built off of http://www.vcskicks.com/randomize_array.php
    private void ShuffleWaves(Wave[] waves)
    {
        int randomIndex = 0;
        ArrayList currentWaves = new ArrayList(waves);
        for (int i = 0; i < numberOfBeginningWavesToRemove; i++)
        {
            currentWaves.RemoveAt(i);
        }
        ArrayList randomizedWaves = new ArrayList();
        while (currentWaves.Count > 0)
        {
            randomIndex = Random.Range(0, currentWaves.Count); //Choose a random object in the list
            randomizedWaves.Add(currentWaves[randomIndex]); //add it to the new, random list
            currentWaves.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        int index = 0;
        waves = new Wave[waves.Length - numberOfBeginningWavesToRemove];
        foreach(Wave wave in randomizedWaves.ToArray())
        {
            waves[index++] = wave;
        }
    }

    public void RageForSeconds(float seconds)
    {
        rageDuration = seconds;
        StartCoroutine(SetRage(seconds));
    }

    public IEnumerator SetRage(float seconds)
    {
        rageMode = true;
        yield return new WaitForSeconds(seconds);
        rageMode = false;
    }

    public void ReleaseScrubLock()
    {
        StartCoroutine(ReleaseScrubLockCoroutine());
    }

    public IEnumerator ReleaseScrubLockCoroutine()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Not yet released");
            yield return new WaitForEndOfFrame();
        }
        GameController.instance.scrubLock = false;
        Debug.Log("Released");
    }

    public void ShakeScreen(float magnitude)
    {
        if (screenShakeMagnitude > magnitude)
        {
            return;
        }
        screenShakeMagnitude = magnitude;
    }
}

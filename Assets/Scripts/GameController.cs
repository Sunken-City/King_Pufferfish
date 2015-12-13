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
	public AudioClip backgroundMusic;
	public GameObject soundPlayer;
	public int numberOfBeginningWavesToRemove = 2;
	public GameObject waveSign;
	public float timeInBetweenWavesSeconds = 4.0f;
	public float waveSpawnTime = 0.0f;
	public GameObject scoreDigit;
	public static bool isSoundMuted = false;
	public static bool isMusicMuted = false;
	public float rageWaveBufferSeconds = 1.0f;
	public float spawnSizeOffset = 0.1f;
	public GameObject TutorialSquid;
	public bool readyToStart = false;
	public GameObject RageWaveInstruction;

	[HideInInspector]
	public bool scrubLock;
	[HideInInspector]
	public bool isRageMode = false;
	[HideInInspector]
	public float rageDuration = 0.0f;
	[HideInInspector]
	public bool isPaused = false;
	[HideInInspector]
	public int score = 0;
	[HideInInspector]
	public float scoreMultiplier = 1.0f;
	[HideInInspector]
	public bool fireLocked = false;
    [HideInInspector]
    public AudioSource musicPlayerSource;

	private float timeRageStarted = 0.0f;
	private float timeRageEnded = 0.0f;
	private float timeSinceLastSpawn = 0.0f;
	private float timeUntilNextSpawn = 2.0f;
	private int currentWaveIndex = 0;
	private int currentWaveNumber = 0;
	private int currentEncounterIndex = 0;
	private bool currentWaveFinished = false;
	private GameObject ambientPlayer;
	private GameObject musicPlayer;
	private AudioSource ambientSoundSource;
	private float screenShakeMagnitude = 0.0f;
	private Vector3 defaultCameraPosition;
	private float screenLeftBounds = -3.124f;
	private float screenRightBounds = 3.134f;
	private float screenTopBounds = 11.87f;
	private float screenBottomBounds = 2.37f;

	void Awake()
	{
		isSoundMuted = (PlayerPrefs.GetInt("SoundMuted", 0) == 0) ? false : true;
		isMusicMuted = (PlayerPrefs.GetInt("MusicMuted", 0) == 0) ? false : true;
		//If we have a second instance, destroy it first
		if (instance)
		{
			Debug.Log("Destroying irrelevant GameController instance");
			Destroy(instance.gameObject);
		}

		instance = this;
		defaultCameraPosition = Camera.main.transform.position;
		timeRageEnded = Time.time - 1.0f;
	}

	// Use this for initialization
	void Start()
	{
		StartMusic();
		if (TutorialSquid)
		{
			GameObject.Instantiate(TutorialSquid);
		}
		else
		{
			StartSpawning();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (ambientSoundSource)
		{
			ambientSoundSource.mute = isMusicMuted;
		}
		if (musicPlayerSource)
		{
			musicPlayerSource.mute = isMusicMuted;
			musicPlayerSource.pitch = isRageMode ? Mathf.Lerp(1.0f, 1.5f, Time.time - timeRageStarted) : Mathf.Lerp(1.5f, 1.0f, Time.time - timeRageEnded);
		}
		ShakeScreen();
		if (!readyToStart)
		{
			if (screenShakeMagnitude > 0.4f)
			{
				GameController.instance.StartSpawning();
			}
			return;
		}
		RunWaves();
	}

	void OnDestroy()
	{
		SaveScore();
		isPaused = false;
		Time.timeScale = 1.0f;
		Destroy(ambientPlayer);
		Destroy(musicPlayer);
		StopAllCoroutines();
	}

	public void StartSpawning()
	{
		timeSinceLastSpawn = 0.0f;
		Debug.Log("Starting wave #" + currentWaveIndex);

		GameObject sign = GameObject.Instantiate(waveSign) as GameObject;
		waveSpawnTime = Time.time;
		sign.GetComponent<WaveSign>().lifeSpan = timeInBetweenWavesSeconds;
		sign.GetComponent<WaveSign>().waveNumber = ++currentWaveNumber;
		Destroy(sign, timeInBetweenWavesSeconds);
		readyToStart = true;
	}

	void StartMusic()
	{
		ambientPlayer = Instantiate(soundPlayer, Vector3.zero, Quaternion.identity) as GameObject;
		ambientSoundSource = ambientPlayer.GetComponent<AudioSource>();
		ambientSoundSource.clip = ambientSound;
		ambientSoundSource.loop = true;
		ambientSoundSource.Play();

		musicPlayer = Instantiate(soundPlayer, Vector3.zero, Quaternion.identity) as GameObject;
		musicPlayerSource = musicPlayer.GetComponent<AudioSource>();
		musicPlayerSource.clip = backgroundMusic;
		musicPlayerSource.volume = 0.75f;
		musicPlayerSource.loop = true;
		musicPlayerSource.Play();
	}

IEnumerator SpawnRageWaveFinger()
{
    yield return new WaitForSeconds(timeInBetweenWavesSeconds);
	GameObject RageWaveFinger = Instantiate(RageWaveInstruction) as GameObject;
	Destroy(RageWaveFinger, 7.0f);
}

	private void RunWaves()
	{
		int startWaveNumber = currentWaveIndex;
		float adjustedTimeSinceFrame = Time.deltaTime * (isRageMode ? rageSpawnMultiplier : 1.0f);
		timeSinceLastSpawn += adjustedTimeSinceFrame;
		if (currentWaveIndex < waves.Length)
		{
			if (timeUntilNextSpawn <= timeSinceLastSpawn && (Time.time - waveSpawnTime) > timeInBetweenWavesSeconds)
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
		if (startWaveNumber != currentWaveIndex && currentWaveIndex < waves.Length)
		{
			GameObject sign = GameObject.Instantiate(waveSign) as GameObject;
			sign.GetComponent<WaveSign>().lifeSpan = timeInBetweenWavesSeconds;
			waveSpawnTime = Time.time;
			sign.GetComponent<WaveSign>().waveNumber = ++currentWaveNumber;
			if (waves[currentWaveIndex].isRageWave)
			{
				sign.GetComponent<WaveSign>().SetSpriteForRage();
			}
			Destroy(sign, timeInBetweenWavesSeconds);
			if (currentWaveNumber == 4 && RageWaveInstruction)
			{
                StartCoroutine(SpawnRageWaveFinger());

			}
		}
	}

	private void ShakeScreen()
	{
		if (!isPaused)
		{
			Vector3 cameraOffset = new Vector3(Random.Range(0.0f, screenShakeMagnitude), Random.Range(0.0f, screenShakeMagnitude), 0.0f);
			Camera.main.transform.position = defaultCameraPosition + cameraOffset;

			screenShakeMagnitude -= Time.deltaTime;
			if (screenShakeMagnitude < 0.0f)
			{
				screenShakeMagnitude = 0.0f;
			}
		}
		else
		{
			Camera.main.transform.position = defaultCameraPosition;
		}
	}

	public void AddScore(int scoreIncrement, Vector3 position)
	{
		score += scoreIncrement;
		if (score < 0)
		{
			score = 0;
		}
		Color digitColor = new Color(1.0f, 1.0f, 1.0f);
        float spawnDurationSeconds = 1.0f;
		if (scoreIncrement < 0)
		{
			digitColor = new Color(1.0f, 0.0f, 0.0f);
            spawnDurationSeconds = 2.2f;
		}
		Bounds digitBounds = scoreDigit.GetComponent<SpriteRenderer>().sprite.bounds;
		float digitLength = digitBounds.extents.x * 2.0f;

		float digitLengthExtents = digitLength * 2.5f;
		float digitHeightExtents = digitBounds.extents.y;

		if (position.x > screenRightBounds - digitLengthExtents)
		{
			position.x = screenRightBounds - digitLengthExtents;
		}
		if (position.x < screenLeftBounds + digitLengthExtents - 0.5f)
		{
			position.x = screenLeftBounds + digitLengthExtents - 0.5f;
		}
		if (position.y > screenTopBounds - digitHeightExtents)
		{
			position.y = screenTopBounds - digitHeightExtents;
		}
		if (position.y < screenBottomBounds + digitHeightExtents)
		{
			position.y = screenBottomBounds + digitHeightExtents;
		}

		position += new Vector3(digitLength * 2.0f, 0.0f, -6.0f);
		GameObject ones = Instantiate(scoreDigit, position, Quaternion.identity) as GameObject;
		ones.GetComponent<Number>().SetDigit(scoreIncrement % 10);
		ones.GetComponent<SpriteRenderer>().color = digitColor;
        Destroy(ones, spawnDurationSeconds);

		position -= new Vector3(digitLength, 0.0f, 0.0f);
		GameObject tens = Instantiate(scoreDigit, position, Quaternion.identity) as GameObject;
		tens.GetComponent<Number>().SetDigit((scoreIncrement / 10) % 10);
		tens.GetComponent<SpriteRenderer>().color = digitColor;
        Destroy(tens, spawnDurationSeconds);

		position -= new Vector3(digitLength, 0.0f, 0.0f);
		GameObject hundreds = Instantiate(scoreDigit, position, Quaternion.identity) as GameObject;
		hundreds.GetComponent<Number>().SetDigit((scoreIncrement / 100) % 10);
		hundreds.GetComponent<SpriteRenderer>().color = digitColor;
        Destroy(hundreds, spawnDurationSeconds);

		if (((scoreIncrement / 1000) % 10) != 0)
		{
			position -= new Vector3(digitLength, 0.0f, 0.0f);
			GameObject thousands = Instantiate(scoreDigit, position, Quaternion.identity) as GameObject;
			thousands.GetComponent<Number>().SetDigit((scoreIncrement / 1000) % 10);
			thousands.GetComponent<SpriteRenderer>().color = digitColor;
            Destroy(thousands, spawnDurationSeconds);
		}

		position -= new Vector3(digitLength, 0.0f, 0.0f);
		GameObject sign = Instantiate(scoreDigit, position, Quaternion.identity) as GameObject;
		sign.GetComponent<Number>().SetSign(scoreIncrement > 0);
		sign.GetComponent<SpriteRenderer>().color = digitColor;
        Destroy(sign, spawnDurationSeconds);
	}

	private void SpawnEnemy(Encounter currentEncounter)
	{
		//Calculate speed constant
		float enemySpeed = currentEncounter.speed;
		if (enemySpeed == 0.0f)
		{
			enemySpeed = defaultEnemySpeed;
		}
		enemySpeed *= difficultySpeedMultiplier;
		enemySpeed *= isRageMode ? rageSpeedMultiplier : 1.0f;

		//Calculate the spawn position and velocity based on where the enemy is going to move.
		Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
		Vector2 enemyVelocity = new Vector2(0.0f, 0.0f);
		bool isFlipped = false;
		if (currentEncounter.movementDirection == MovementDirection.DOWN)
		{
			if (isRageMode)
			{
				spawnPosition = new Vector3(Random.Range(-3.0f, 3.0f), 14.0f, 0.0f);
			}
			else
			{
				spawnPosition = new Vector3(currentEncounter.spawnPositionOffset, 14.0f, 0.0f);
			}
			enemyVelocity = new Vector2(0.0f, -enemySpeed);
		}
		else if (currentEncounter.movementDirection == MovementDirection.LEFT_TO_RIGHT)
		{
			spawnPosition = new Vector3(-4.5f, currentEncounter.spawnPositionOffset + 8.0f, 0.0f);
			enemyVelocity = new Vector2(enemySpeed, 0.0f);
			isFlipped = true;
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
		if (isFlipped == true)
		{
		Vector3 theScale = enemy.transform.localScale;
		theScale.x *= -1;
		enemy.transform.localScale = theScale;
		}
		if (!enemy.GetComponent<UnderwaterMine>() && !enemy.GetComponent<Heart>())
		{
			float offset = Random.Range(-spawnSizeOffset, spawnSizeOffset);
			enemy.transform.localScale *= (1.0f + offset);
		}
		if (enemy.GetComponent<Squid>())
		{
			enemy.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
		}
		else if (isRageMode && enemy.GetComponent<UnderwaterMine>())
		{
			Destroy(enemy);
			Debug.Log("Mine prevented from spawning because of rage mode");
		}
	}

	private void SpawnFromWave()
	{
		Wave currentWave = waves[currentWaveIndex];
		if (currentWave.isRageWave)
		{
			if (timeRageStarted == 0.0f)
			{
				RageForSeconds(currentWave.durationSeconds + rageWaveBufferSeconds);
				timeRageStarted = Time.time;
			}
			int encounterIndex = Random.Range(0, currentWave.EncounterList.Length);
			Encounter currentEncounter = currentWave.EncounterList[encounterIndex];
			SpawnEnemy(currentEncounter);
			timeSinceLastSpawn = 0.0f;
			timeUntilNextSpawn = 1.0f / currentWave.spawnRate;
			if (Time.time - timeRageStarted >= currentWave.durationSeconds)
			{
				currentWaveFinished = true;
				timeRageStarted = 0.0f;
				Debug.Log("Wave Finished");
			}
		}
		else if (currentEncounterIndex < currentWave.EncounterList.Length)
		{
			Debug.Log("Spawning Encounter #" + currentEncounterIndex);
			Encounter currentEncounter = currentWave.EncounterList[currentEncounterIndex];
			SpawnEnemy(currentEncounter);          
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

	public void PlayRandomSound(AudioClip[] soundList)
	{
		if (soundList.Length > 0)
		{
			int index = Random.Range(0, soundList.Length);
			if (soundList[index] != null)
				PlaySound(soundList[index]);
		}
	}

	public void PlaySound(AudioClip sound)
	{
		if (!isSoundMuted)
		{
			StartCoroutine(PlaySoundAsync(sound));            
		}
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
		isRageMode = true;
		yield return new WaitForSeconds(seconds);
		isRageMode = false;
		timeRageEnded = Time.time;
	}

	public void ReleaseScrubLock()
	{
		StartCoroutine(ReleaseScrubLockCoroutine());
	}

	public IEnumerator ReleaseScrubLockCoroutine()
	{
		while (Input.GetKey(KeyCode.Mouse0))
		{
			yield return new WaitForEndOfFrame();
		}
		GameController.instance.scrubLock = false;
	}

	public void LockFiringForSeconds(float seconds)
	{
		StartCoroutine(LockForSeconds(seconds));
	}

	IEnumerator LockForSeconds(float seconds)
	{
		fireLocked = true;
		yield return new WaitForSeconds(seconds);
		fireLocked = false;
	}

	public void ShakeScreen(float magnitude)
	{
		if (screenShakeMagnitude > magnitude)
		{
			return;
		}
		screenShakeMagnitude = magnitude;
	}

	public void TogglePause()
	{
		isPaused = !isPaused;
		Time.timeScale = isPaused ? 0.0f : 1.0f;
		LockFiringForSeconds(0.1f);
	}

	public static void ToggleSoundMute()
	{
		isSoundMuted = !isSoundMuted;
		int isMuted = isSoundMuted ? 1 : 0;
		PlayerPrefs.SetInt("SoundMuted", isMuted);
		PlayerPrefs.Save();
	}

	public static void ToggleMusicMute()
	{
		isMusicMuted = !isMusicMuted;
		int isMuted = isMusicMuted ? 1 : 0;
		PlayerPrefs.SetInt("MusicMuted", isMuted);
		PlayerPrefs.Save();
	}

	public void SaveScore()
	{
		PlayerPrefs.SetInt("GameScore", score);
	}

	public void ResetMultiplier()
	{
		scoreMultiplier = 1.0f;
	}

	public void IncrementMultiplier()
	{
		if (scoreMultiplier < 5.0f)
		{
			scoreMultiplier += 1.0f;
		}
		else if (scoreMultiplier < 8.0f)
		{
			scoreMultiplier += 0.5f;
		}
		else if (scoreMultiplier < 10.0f)
		{
			scoreMultiplier += 0.1f;
		}
		//Multiplier at 10, don't increment.
	}
}

using UnityEngine;
using System.Collections;

public enum MovementDirection
{
    DOWN,
    LEFT_TO_RIGHT,
    RIGHT_TO_LEFT
}

[System.Serializable]
public class Encounter
{
    public GameObject enemy;
    public float waitSecondsAfterSpawn = 1.0f;
    [Range(-3.0f, 3.0f)]
    public float spawnPositionOffset = 0.0f;
    public float speed = 1.0f;
    public MovementDirection movementDirection = MovementDirection.DOWN;
}

public class Wave : ScriptableObject
{
    public bool isRageWave = false;
    public float spawnRate = 3.0f;
    public float durationSeconds = 10.0f;
    public Encounter[] EncounterList;
}
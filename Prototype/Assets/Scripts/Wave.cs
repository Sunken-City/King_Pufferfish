using UnityEngine;
using System.Collections;

[System.Serializable]
public class Encounter
{
    public GameObject enemy;
    public float waitSecondsAfterSpawn = 1.0f;
    [Range(-3.0f, 3.0f)]
    public float spawnPositionX = 0.0f;
    public float speed = 1.0f;
    public float size = 1.0f;
    public int health = 1;
}

public class Wave : ScriptableObject
{
    public Encounter[] EncounterList;
}

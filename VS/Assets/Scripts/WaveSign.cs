using UnityEngine;
using System.Collections;

public class WaveSign : MonoBehaviour {

    public Number waveNumberTens;
    public Number waveNumberOnes;
    public Sprite rageWaveSprite;

    [HideInInspector]
    public float lifeSpan;
    [HideInInspector]
    public int waveNumber;

    private float timer;
    private float halfLifespan;

	// Use this for initialization
	void Start () 
    {
        waveNumberTens.SetDigit(waveNumber / 10);
        waveNumberOnes.SetDigit(waveNumber % 10);
        if (waveNumber / 10 == 0)
        {
            waveNumberTens.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        halfLifespan = lifeSpan / 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        float t = timer - halfLifespan;
        float tSquared = t * t;
        float t4 = tSquared * tSquared;
        transform.position = new Vector3(0.0f, (t4) + 5.0f, -4.0f);
	}

    public void SetSpriteForRage()
    {
        GetComponent<SpriteRenderer>().sprite = rageWaveSprite;
    }
}

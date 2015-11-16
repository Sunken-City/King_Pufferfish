using UnityEngine;
using System.Collections;

public class GameOverScoreDisplay : MonoBehaviour 
{
    public GameObject digit;
    private int score;
	// Use this for initialization
	void Start () 
    {
        score = PlayerPrefs.GetInt("GameScore");

        Bounds digitBounds = digit.GetComponent<SpriteRenderer>().sprite.bounds;
        float digitLength = digitBounds.extents.x * 2.0f;
        Vector3 position = transform.position;
        GameObject ones = Instantiate(digit, position, Quaternion.identity) as GameObject;
        ones.GetComponent<Number>().SetDigit(score % 10);
        position -= new Vector3(digitLength, 0.0f, 0.0f);
        GameObject tens = Instantiate(digit, position, Quaternion.identity) as GameObject;
        tens.GetComponent<Number>().SetDigit((score / 10) % 10);
        position -= new Vector3(digitLength, 0.0f, 0.0f);
        GameObject hundreds = Instantiate(digit, position, Quaternion.identity) as GameObject;
        hundreds.GetComponent<Number>().SetDigit((score / 100) % 10);
        position -= new Vector3(digitLength, 0.0f, 0.0f);
        GameObject thousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
        thousands.GetComponent<Number>().SetDigit((score / 1000) % 10);
        position -= new Vector3(digitLength, 0.0f, 0.0f);
        GameObject tenThousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
        tenThousands.GetComponent<Number>().SetDigit((score / 10000) % 10);
        position -= new Vector3(digitLength, 0.0f, 0.0f);
        GameObject hundredThousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
        hundredThousands.GetComponent<Number>().SetDigit((score / 100000) % 10);
        position -= new Vector3(digitLength, 0.0f, 0.0f);
        GameObject millions = Instantiate(digit, position, Quaternion.identity) as GameObject;
        millions.GetComponent<Number>().SetDigit((score / 1000000) % 10);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}

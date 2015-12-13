using UnityEngine;
using System.Collections;

public class GameOverScoreDisplay : MonoBehaviour 
{
	public GameObject digit;
	private int playerScore;
	private int highScore;
	private bool newHighScore = false;

	private void ModifyScoreDigits(int score, Vector3 position, float scale, Color digitColor)
	{
		Bounds digitBounds = digit.GetComponent<SpriteRenderer>().sprite.bounds;
		float digitLength = digitBounds.extents.x * 2.0f * scale;
		
		GameObject ones = Instantiate(digit, position, Quaternion.identity) as GameObject;
		ones.GetComponent<Number>().SetDigit(score % 10);
		ones.transform.localScale = new Vector3(scale, scale, scale);
		ones.GetComponent<SpriteRenderer>().color = digitColor;

		if ((score > 10) || ((score / 10) % 10 != 0))
		{
			position -= new Vector3(digitLength, 0.0f, 0.0f);
			GameObject tens = Instantiate(digit, position, Quaternion.identity) as GameObject;
			tens.GetComponent<Number>().SetDigit((score / 10) % 10);
			tens.transform.localScale = new Vector3(scale, scale, scale);
			tens.GetComponent<SpriteRenderer>().color = digitColor;
		}

		if ((score > 100) || ((score / 100) % 10 != 0))
		{
			position -= new Vector3(digitLength, 0.0f, 0.0f);
			GameObject hundreds = Instantiate(digit, position, Quaternion.identity) as GameObject;
			hundreds.GetComponent<Number>().SetDigit((score / 100) % 10);
			hundreds.transform.localScale = new Vector3(scale, scale, scale);
			hundreds.GetComponent<SpriteRenderer>().color = digitColor;
		}

		if ((score > 1000) || ((score / 1000) % 10 != 0))
		{
			position -= new Vector3(digitLength, 0.0f, 0.0f);
			GameObject thousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
			thousands.GetComponent<Number>().SetDigit((score / 1000) % 10);
			thousands.transform.localScale = new Vector3(scale, scale, scale);
			thousands.GetComponent<SpriteRenderer>().color = digitColor;
		}

		if ((score > 10000) || ((score / 10000) % 10 != 0))
		{
			position -= new Vector3(digitLength, 0.0f, 0.0f);
			GameObject tenThousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
			tenThousands.GetComponent<Number>().SetDigit((score / 10000) % 10);
			tenThousands.transform.localScale = new Vector3(scale, scale, scale);
			tenThousands.GetComponent<SpriteRenderer>().color = digitColor;
		}

		if ((score > 100000) || ((score / 100000) % 10 != 0))
		{
			position -= new Vector3(digitLength, 0.0f, 0.0f);
			GameObject hundredThousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
			hundredThousands.GetComponent<Number>().SetDigit((score / 100000) % 10);
			hundredThousands.transform.localScale = new Vector3(scale, scale, scale);
			hundredThousands.GetComponent<SpriteRenderer>().color = digitColor;
		}

		if ((score > 1000000) || ((score / 1000000) % 10 != 0))
		{
			position -= new Vector3(digitLength, 0.0f, 0.0f);
			GameObject millions = Instantiate(digit, position, Quaternion.identity) as GameObject;
			millions.GetComponent<Number>().SetDigit((score / 1000000) % 10);
			millions.transform.localScale = new Vector3(scale, scale, scale);
			millions.GetComponent<SpriteRenderer>().color = digitColor;
		}

		if ((score > 10000000) || ((score / 10000000) % 10 != 0))
		{
			position -= new Vector3(digitLength, 0.0f, 0.0f);
			GameObject tenMillions = Instantiate(digit, position, Quaternion.identity) as GameObject;
			tenMillions.GetComponent<Number>().SetDigit((score / 10000000) % 10);
			tenMillions.transform.localScale = new Vector3(scale, scale, scale);
			tenMillions.GetComponent<SpriteRenderer>().color = digitColor;
		}
	}

	// Use this for initialization
	void Start () 
	{
		playerScore = PlayerPrefs.GetInt("GameScore");
		highScore = PlayerPrefs.GetInt("HighScore", 0);
		if (playerScore > highScore)
		{
			newHighScore = true;
			PlayerPrefs.SetInt("HighScore", playerScore);
			highScore = playerScore;
		}

		ModifyScoreDigits(playerScore, transform.position, 1.0f, Color.green);
		ModifyScoreDigits(highScore, transform.position + new Vector3(0.0f, 1.425f, 0.0f), 0.5f, Color.green);

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

using UnityEngine;
using System.Collections;

public class PlayerScoreDisplay : MonoBehaviour 
{
	public GameObject digit;
	private int score;

	private GameObject ones;
	private GameObject tens;
	private GameObject hundreds;
	private GameObject thousands;
	private GameObject tenThousands;
	private GameObject hundredThousands;
	private GameObject millions;
	private GameObject tenMillions;

	private float digitLength;
	private Vector3 position;

	// Use this for initialization
	void Start () 
	{
		Bounds digitBounds = digit.GetComponent<SpriteRenderer>().sprite.bounds;
		digitLength = digitBounds.extents.x;
		position = transform.position;

		Vector3 scoreTextPosition = GameObject.Find("ScoreText").transform.position;
		GameObject.Find("ScoreText").transform.position = new Vector3(position.x - 1.0f, scoreTextPosition.y, scoreTextPosition.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
		score = GameController.instance.score;

		Vector3 scoreTextPosition = GameObject.Find("ScoreText").transform.position;

		if(score >= 0)
		{
			if (!ones)
			{
				ones = Instantiate(digit, position, Quaternion.identity) as GameObject;
				ones.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				ones.transform.parent = this.gameObject.transform;
			}
			
			ones.GetComponent<Number>().SetDigit(score % 10);
		}

		if(score >= 10)
		{
			if (!tens)
			{
				position -= new Vector3(digitLength, 0.0f, 0.0f);
				scoreTextPosition = new Vector3(scoreTextPosition.x - 0.28f, scoreTextPosition.y, scoreTextPosition.z);
				tens = Instantiate(digit, position, Quaternion.identity) as GameObject;
				tens.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				tens.transform.parent = this.gameObject.transform;
			}
			
			tens.GetComponent<Number>().SetDigit((score / 10) % 10);
		}

		if (score >= 100)
		{
			if (!hundreds)
			{
				position -= new Vector3(digitLength, 0.0f, 0.0f);
				scoreTextPosition = new Vector3(scoreTextPosition.x - 0.28f, scoreTextPosition.y, scoreTextPosition.z);
				hundreds = Instantiate(digit, position, Quaternion.identity) as GameObject;
				hundreds.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				hundreds.transform.parent = this.gameObject.transform;
			}
			
			hundreds.GetComponent<Number>().SetDigit((score / 100) % 10);
		}

		if (score >= 1000)
		{
			if (!thousands)
			{
				position -= new Vector3(digitLength, 0.0f, 0.0f);
				scoreTextPosition = new Vector3(scoreTextPosition.x - 0.28f, scoreTextPosition.y, scoreTextPosition.z);
				thousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
				thousands.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				thousands.transform.parent = this.gameObject.transform;
			}
			
			thousands.GetComponent<Number>().SetDigit((score / 1000) % 10);
		}

		if (score >= 10000)
		{
			if (!tenThousands)
			{
				position -= new Vector3(digitLength, 0.0f, 0.0f);
				scoreTextPosition = new Vector3(scoreTextPosition.x - 0.28f, scoreTextPosition.y, scoreTextPosition.z);
				tenThousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
				tenThousands.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				tenThousands.transform.parent = this.gameObject.transform;
			}
			
			tenThousands.GetComponent<Number>().SetDigit((score / 10000) % 10);
		}

		if (score >= 100000)
		{
			if (!hundredThousands)
			{
				position -= new Vector3(digitLength, 0.0f, 0.0f);
				scoreTextPosition = new Vector3(scoreTextPosition.x - 0.28f, scoreTextPosition.y, scoreTextPosition.z);
				hundredThousands = Instantiate(digit, position, Quaternion.identity) as GameObject;
				hundredThousands.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				hundredThousands.transform.parent = this.gameObject.transform;
			}
			
			hundredThousands.GetComponent<Number>().SetDigit((score / 100000) % 10);
		}

		if (score >= 1000000)
		{
			if (!millions)
			{
				position -= new Vector3(digitLength, 0.0f, 0.0f);
				scoreTextPosition = new Vector3(scoreTextPosition.x - 0.28f, scoreTextPosition.y, scoreTextPosition.z);
				millions = Instantiate(digit, position, Quaternion.identity) as GameObject;
				millions.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				millions.transform.parent = this.gameObject.transform;
			}
			
			millions.GetComponent<Number>().SetDigit((score / 1000000) % 10);
		}

		if (score >= 10000000)
		{
			if (!tenMillions)
			{
				position -= new Vector3(digitLength, 0.0f, 0.0f);
				scoreTextPosition = new Vector3(scoreTextPosition.x - 0.28f, scoreTextPosition.y, scoreTextPosition.z);
				tenMillions = Instantiate(digit, position, Quaternion.identity) as GameObject;
				tenMillions.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				tenMillions.transform.parent = this.gameObject.transform;
			}
			
			tenMillions.GetComponent<Number>().SetDigit((score / 10000000) % 10);
		}

		GameObject.Find("ScoreText").transform.position = scoreTextPosition;
	}
}

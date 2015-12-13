using UnityEngine;
using System.Collections;

public class ScoreMultiplierUI : MonoBehaviour {

    public Number numberTens;
    public Number numberOnes;
    public Number numberTenths;
    public GameObject multiplierX;

    private float numberBounds;
    private Vector3 multiplierPosition;

	// Use this for initialization
	void Start () 
    {
        multiplierPosition = multiplierX.transform.position;
        numberBounds = numberTens.gameObject.GetComponent<SpriteRenderer>().bounds.extents.x * 2.0f;
        UpdateMutliplier();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMutliplier();
	}

    int GetScoreAsInt()
    {
        return (int)(GameController.instance.scoreMultiplier * 10.0f);
    }

    void UpdateMutliplier()
    {
        int scoreInt = GetScoreAsInt();
        //We shift the numbers up one place
        numberTens.SetDigit((scoreInt / 100) % 10);
        numberOnes.SetDigit((scoreInt / 10) % 10);
        numberTenths.SetDigit(scoreInt % 10);
        if (scoreInt / 100 == 0)
        {
            numberTens.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            multiplierX.transform.position = new Vector3(multiplierPosition.x + numberBounds, multiplierPosition.y, multiplierPosition.z);
        }
        else
        {
            numberTens.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            multiplierX.transform.position = new Vector3(multiplierPosition.x, multiplierPosition.y, multiplierPosition.z);
        }
    }

}

using UnityEngine;
using System.Collections;

public class Number : MonoBehaviour {

    public Sprite[] digits;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetDigit(int zeroThroughNine)
    {
        zeroThroughNine = Mathf.Abs(zeroThroughNine);
        if (zeroThroughNine > 9 || zeroThroughNine < 0)
        {
            Debug.LogError("Number prefab was told to SetDigit to a number out of range. Please enter a number from 0 to 9");
        }
        GetComponent<SpriteRenderer>().sprite = digits[zeroThroughNine];
    }


    public void SetSign(bool isPositive)
    {
        int signDigit = isPositive ? 10 : 11;
        GetComponent<SpriteRenderer>().sprite = digits[signDigit];
    }
}

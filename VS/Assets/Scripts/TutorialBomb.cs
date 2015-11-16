using UnityEngine;
using System.Collections;

public class TutorialBomb : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, Mathf.PingPong( Time.time, 1));

	}
}

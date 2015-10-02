using UnityEngine;
using System.Collections;

public class GrassKillPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        if (WhoCollidedWithMe.tag == "Player")
        {
            Application.LoadLevel("FlappyBurd");
        }
    }
}

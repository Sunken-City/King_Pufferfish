using UnityEngine;
using System.Collections;

public class FingerTap : MonoBehaviour {

	public float speed = 0.1f;
	public float deltaScale = 0.02f;
	public float minimumSize = 0.08f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.localScale = new Vector3(Mathf.PingPong(Time.time * speed, deltaScale) + minimumSize, Mathf.PingPong(Time.time * speed, deltaScale) + minimumSize);
	}
}

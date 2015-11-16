using UnityEngine;
using System.Collections;

public class FingerSwipe : MonoBehaviour {
	public float speed;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.parent.transform.localRotation = Quaternion.identity;
		transform.position = new Vector3 (Mathf.PingPong (Time.time * speed, -.9f) + 1.0f, transform.position.y, transform.position.z);
	}
}
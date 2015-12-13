using UnityEngine;
using System.Collections;

public class FingerSwipe : MonoBehaviour {
	public float speed;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent)
        {
            transform.parent.transform.localRotation = Quaternion.identity;            
        }
        //transform.localPosition = new Vector3(Mathf.PingPong(Time.time * speed, -3.0f) + 4.5f, transform.localPosition.y, transform.localPosition.z);
	}
}
using UnityEngine;
using System.Collections;

public class FadeAndDie : MonoBehaviour {

    public float lifetime = 1.0f;
    private SpriteRenderer renderer;
    private float age = 0.0f;
	// Use this for initialization
	void Start () 
    {
        renderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        age += Time.deltaTime;
	    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, Mathf.Lerp(1.0f, 0.0f, age / lifetime));
        if ((age / lifetime) >= 1.0f)
        {
            Destroy(this.gameObject);
        }
	}
}

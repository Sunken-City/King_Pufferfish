using UnityEngine;
using System.Collections;

public class WarningSign : MonoBehaviour {

    public float lifespan = 1.0f;
    private SpriteRenderer sprite;
    private float halfLifespan;
    private float timer = 0.0f;
	// Use this for initialization
	void Start () 
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.0f);
        halfLifespan = lifespan / 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        if (timer < halfLifespan)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(0.0f, 1.0f, timer / halfLifespan));
        }
        else if (timer < lifespan)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(1.0f, 0.0f, (timer / halfLifespan) - 1));
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
}

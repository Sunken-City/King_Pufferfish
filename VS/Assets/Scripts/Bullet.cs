using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private float screenLeftBounds = -3.124f;
    private float screenRightBounds = 3.134f;

    private float screenTopBounds = 11.87f;
    private float screenBottomBounds = 2.37f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < screenLeftBounds || transform.position.x > screenRightBounds || transform.position.y < screenBottomBounds || transform.position.y > screenTopBounds)
        {
           Destroy(this.gameObject);
        }
    }
}
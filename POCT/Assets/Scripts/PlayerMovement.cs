using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float forceUp = 5.0f;
    public float forceRight = 1.0f;
    private int tapCount;
    private int pipeCount;

    // Use this for initialization
    void Start()
    {
        tapCount = 0;
        pipeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(forceRight, forceUp), ForceMode2D.Impulse);
            tapCount++;
        }
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(forceRight, forceUp), ForceMode2D.Impulse);
            tapCount++;
        }
        GameObject.Find("TapCountText").GetComponent<Text>().text = tapCount.ToString();
        GameObject.Find("PipeCountText").GetComponent<Text>().text = pipeCount.ToString();
    }
    void OnTriggerEnter2D(Collider2D WhoCollidedWithMe)
    {
        if (WhoCollidedWithMe.tag == "PipeCounter")
        {
            pipeCount++;
        }
    }
}

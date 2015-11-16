using UnityEngine;
using System.Collections;

public class BackgroundColorChange : MonoBehaviour {

    private bool inRageMode = false;
    private Color normalColor;
    private Color rageColor = new Color(1.0f, 100.0f / 255.0f, 100.0f / 255.0f);
    private SpriteRenderer renderer;
	// Use this for initialization
	void Start () 
    {
        renderer = GetComponent<SpriteRenderer>();
        normalColor = renderer.color;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GameController.instance.isRageMode != inRageMode)
        {
            inRageMode = GameController.instance.isRageMode;
            if (inRageMode)
            {
                StartCoroutine("TintForRageMode");
            }
        }
	}

    IEnumerator TintForRageMode()
    {
        float durationSeconds = GameController.instance.rageDuration / 4.0f;
        float timeStart = Time.time;
        float timeElapsed = Time.time - timeStart;
        while (timeElapsed < durationSeconds)
        {
            float g = Mathf.Lerp(normalColor.g, rageColor.g, timeElapsed / durationSeconds);
            float b = Mathf.Lerp(normalColor.b, rageColor.b, timeElapsed / durationSeconds);
            renderer.color = new Color(normalColor.r, g, b, normalColor.a);
            yield return new WaitForEndOfFrame();
            timeElapsed = Time.time - timeStart;
        }

        yield return new WaitForSeconds(GameController.instance.rageDuration / 2.0f);

        timeStart = Time.time;
        timeElapsed = Time.time - timeStart;
        while (timeElapsed < durationSeconds)
        {
            float g = Mathf.Lerp(rageColor.g, normalColor.g, timeElapsed / durationSeconds);
            float b = Mathf.Lerp(rageColor.b, normalColor.b, timeElapsed / durationSeconds);
            renderer.color = new Color(normalColor.r, g, b, normalColor.a);
            yield return new WaitForEndOfFrame();
            timeElapsed = Time.time - timeStart;
        }
    }
}

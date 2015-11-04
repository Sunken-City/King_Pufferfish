using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Ink : MonoBehaviour
{
	public bool mouseOver = false;
	//Higher resistance means harder to scrub.
	public float inkScrubResistance = 10.0f;

	static bool mouseDown = false;
	static Vector2 previousMousePosition;
    static Camera sceneCamera;
    public AudioClip SqueakForward;
    public AudioClip SqueakBack;

    private static int inkCount = 0;
    private static float timeSinceLastSqueak = 0.0f;
    private bool squeakForward = false;

    void Awake()
    {
        inkCount++;
        timeSinceLastSqueak = Time.time;
    }

	// Use this for initialization
	void Start () 
	{
		sceneCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		//We start up a coroutine that runs in the background, grabbing the mouse's new position every 1/10th of a second.
		StartCoroutine ("GetPreviousMousePosition");
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKey(KeyCode.Mouse0)) 
		{
			Vector2 currentMousePosition = (Vector2)sceneCamera.ScreenToWorldPoint(Input.mousePosition);
			if(mouseOver)
			{
				Color inkColor = GetComponent<SpriteRenderer>().color;
				//Take our current mouse position and subtract the previous position to get a vector between the two
				Vector2 mouseDistanceTravelledThisFrame = currentMousePosition - previousMousePosition;
                bool scrubForward = squeakForward;
                if (mouseDistanceTravelledThisFrame.x + mouseDistanceTravelledThisFrame.y > 0)
                {
                    scrubForward = true;
                }
                else if (mouseDistanceTravelledThisFrame.x + mouseDistanceTravelledThisFrame.y < 0)
                {
                    scrubForward = false;
                }
                if (scrubForward != squeakForward)
                {
                    squeakForward = scrubForward;
                    timeSinceLastSqueak = Time.time;
                    if (squeakForward)
                    {
                        GameController.instance.PlaySound(SqueakForward);
                    }
                    else
                    {
                        GameController.instance.PlaySound(SqueakBack);
                    }
                }
				//Divide the magnitude by the resistance value. Subtract this value from the ink's alpha value
                float swipeStrength = mouseDistanceTravelledThisFrame.magnitude / inkScrubResistance;
				inkColor.a -= swipeStrength;
                GameController.instance.scrubLock = true;
				SprayBulletScript.ChargeUp(swipeStrength);
				GetComponent<SpriteRenderer>().color = inkColor;
				if (inkColor.a <= 0.0f) 
				{
					Destroy(this.gameObject);
				}
			}
		}
        else
        {
            GameController.instance.scrubLock = false;
        }
	}

    public void OnDestroy()
    {
        inkCount--;
        if (inkCount == 0 && GameController.instance)
        {
            GameController.instance.ReleaseScrubLock();
        }
    }

	public IEnumerator GetPreviousMousePosition()
	{
		while (true) 
		{
			previousMousePosition = (Vector2)sceneCamera.ScreenToWorldPoint (Input.mousePosition);
			yield return new WaitForSeconds (0.1f);
		}
	}

	public void OnMouseDown()
	{
		mouseDown = true;
	}
	
	public void OnMouseUp()
	{
		mouseDown = false;
        GameController.instance.scrubLock = false;
	}

	void OnMouseEnter() {
		mouseOver = true;
	}
	
	void OnMouseExit() {
		mouseOver = false;
	}
}

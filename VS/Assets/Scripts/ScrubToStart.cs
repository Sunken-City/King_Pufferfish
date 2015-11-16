using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrubToStart : MonoBehaviour
{
	public bool mouseOver = false;
	//Higher resistance means harder to scrub.
	public float inkScrubResistance = 10.0f;

	static bool mouseDown = false;
	static Vector2 previousMousePosition;
	static Camera sceneCamera;

	void Awake()
	{
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
				//Divide the magnitude by the resistance value. Subtract this value from the ink's alpha value
				float swipeStrength = mouseDistanceTravelledThisFrame.magnitude / inkScrubResistance;
				inkColor.a -= swipeStrength;
				GetComponent<SpriteRenderer>().color = inkColor;
				if (inkColor.a <= 0.0f) 
				{
					Destroy(this.gameObject);
				}
			}
		}
	}

	public void OnDestroy()
	{
		StopAllCoroutines();
		Application.LoadLevelAsync("MainScene");
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
	}

	void OnMouseEnter() {
		mouseOver = true;
	}
	
	void OnMouseExit() {
		mouseOver = false;
	}
}

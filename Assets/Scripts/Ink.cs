using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Ink : MonoBehaviour
{
	public bool mouseOver = false;

	static bool mouseDown = false;
	static Vector2 previousMousePosition;
	static Camera sceneCamera;
	// Use this for initialization
	void Start () 
	{
		sceneCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		StartCoroutine ("GetPreviousMousePosition");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mouseDown) 
		{
			Vector2 currentMousePosition = (Vector2)sceneCamera.ScreenToWorldPoint(Input.mousePosition);
			if(mouseOver)
			{
				Color inkColor = GetComponent<SpriteRenderer>().color;

				Vector2 mouseDistanceTravelledThisFrame = currentMousePosition - previousMousePosition;
				float swipeStrength = mouseDistanceTravelledThisFrame.magnitude / 10.0f;
				Debug.Log(currentMousePosition + " - " + previousMousePosition + " = " + swipeStrength);
				inkColor.a -= swipeStrength;
				GetComponent<SpriteRenderer>().color = inkColor;
				if (inkColor.a <= 0.0f) 
				{
					Destroy(this.gameObject);
				}
			}
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
	}

	void OnMouseEnter() {
		mouseOver = true;
	}
	
	void OnMouseExit() {
		mouseOver = false;
	}
}

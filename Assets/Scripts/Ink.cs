using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Ink : MonoBehaviour
{
	public bool mouseDown = false;
	public bool mouseOver = false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mouseDown && mouseOver) 
		{
			Color inkColor = GetComponent<SpriteRenderer>().color;
			inkColor.a -= 0.01f;
			GetComponent<SpriteRenderer>().color = inkColor;
			if (inkColor.a <= 0.0f) 
			{
				Destroy(this.gameObject);
			}
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

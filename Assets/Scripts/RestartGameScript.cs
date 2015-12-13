using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RestartGameScript : MonoBehaviour {

	private bool mouseOver = false;
	private Color buttonTint = Color.gray;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

	public void OnMouseDown()
	{
        GetComponent<SpriteRenderer>().color = buttonTint;
        Application.LoadLevelAsync("MainScene");
	}

}

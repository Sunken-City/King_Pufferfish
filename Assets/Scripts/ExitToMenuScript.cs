using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ExitToMenuScript : MonoBehaviour {

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
		Application.LoadLevelAsync("GameStartMenu");
		GetComponent<SpriteRenderer>().color = buttonTint;
	}

}

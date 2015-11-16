using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StartGameScript : MonoBehaviour {

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
		if (!SubMenuButtonScript.SubMenuIsOpen())
		{
			GetComponent<SpriteRenderer>().color = buttonTint;
			Application.LoadLevelAsync("MainScene");
		}
	}

}

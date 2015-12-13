using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ResumeButtonScript : MonoBehaviour {

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
		if (!QuitDialogScript.QuitDialogIsOpen())
		{
			GetComponent<SpriteRenderer>().color = buttonTint;
			GameController.instance.TogglePause();
			Destroy(this.transform.parent.gameObject);
		}
	}

}

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseButtonScript : MonoBehaviour {

	public GameObject pauseMenu;

	private GameObject pauseMenuInstance;
	private bool mouseOver = false;

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
			GameController.instance.TogglePause();

			if (GameController.instance.isPaused)
			{
				pauseMenuInstance = Instantiate(pauseMenu, new Vector3(0.0f, 7.35f, -7.0f), Quaternion.identity) as GameObject;
			}
			else
			{
				Destroy(pauseMenuInstance);
			}
		}
	}
}

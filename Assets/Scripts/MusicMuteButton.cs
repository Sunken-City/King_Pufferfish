using UnityEngine;
using System.Collections;

public class MusicMuteButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameController.isMusicMuted)
		{
			GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		}
		else
		{
			GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseDown()
	{
		if (!QuitDialogScript.QuitDialogIsOpen())
		{
			GameController.ToggleMusicMute();
			if (GameController.isMusicMuted)
			{
				GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			}
			else
			{
				GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
		}
	}

}

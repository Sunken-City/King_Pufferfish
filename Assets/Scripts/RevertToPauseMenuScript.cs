using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RevertToPauseMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void OnMouseDown()
	{
		Destroy(this.transform.parent.gameObject);
		QuitDialogScript.SetQuitDialogOpen(false);
	}

}

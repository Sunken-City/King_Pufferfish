using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class QuitDialogScript : MonoBehaviour {

	public GameObject quitDialog;
	private GameObject quitDialogInstance;

	private static bool quitDialogOpen = false;

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
		if(!QuitDialogIsOpen())
		{
			quitDialogInstance = Instantiate(quitDialog, new Vector3(0.0f, 7.35f, -8.0f), Quaternion.identity) as GameObject;
			SetQuitDialogOpen(true);
		}
	}

	public static bool QuitDialogIsOpen()
	{
		return quitDialogOpen;
	}

	public static void SetQuitDialogOpen(bool open)
	{
		quitDialogOpen = open;
	}

	void OnDestroy()
	{
		quitDialogOpen = false;
	}
}

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SubMenuButtonScript : MonoBehaviour {

	public GameObject SubMenuScreen;

	private bool mouseOver = false;
	private static bool subMenuOpen = false;

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
		if (!SubMenuIsOpen())
		{
			GameObject.Instantiate(SubMenuScreen, new Vector3(0.0f, 0.0f, -0.2f), Quaternion.identity);
			SetSubMenuOpen(true);
		}
	}

	public static bool SubMenuIsOpen()
	{
		return subMenuOpen;
	}

	public static void SetSubMenuOpen(bool open)
	{
		subMenuOpen = open;
	}
}

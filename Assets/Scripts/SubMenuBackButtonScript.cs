using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SubMenuBackButtonScript : MonoBehaviour {

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
		SubMenuButtonScript.SetSubMenuOpen(false);
	}

}

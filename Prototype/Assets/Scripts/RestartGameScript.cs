using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RestartGameScript : MonoBehaviour {

    private bool mouseOver = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnMouseDown()
    {
        
        if (mouseOver)
        {
            Application.LoadLevel("POCT Combined");
        }
    }
    public void OnMouseEnter()
    {
        mouseOver = true;
    }
    public void OnMouseExit()
    {
        mouseOver = false;
    }
}

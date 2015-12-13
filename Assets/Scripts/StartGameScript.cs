using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StartGameScript : MonoBehaviour 
{

	private Color buttonTint = Color.gray;

	public GameObject LoadingScreen;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

	public void OnMouseDown()
	{
		if (!SubMenuButtonScript.SubMenuIsOpen())
		{
			GetComponent<SpriteRenderer>().color = buttonTint;
			GameObject.Instantiate(LoadingScreen, new Vector3(0.0f, 0.0f, -1.0f), Quaternion.identity);
            int isNewPlayer = PlayerPrefs.GetInt("NewPlayer", 0);
            if (isNewPlayer == 0)
            {
                StartCoroutine(WaitForSecondsAndLoad(5.0f));
                PlayerPrefs.SetInt("NewPlayer", 1);
            }
            else
            {
                StartCoroutine(WaitForSecondsAndLoad(0.0f));
            }
		}
	}

    IEnumerator WaitForSecondsAndLoad(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.LoadLevelAsync("MainScene");
    }

}

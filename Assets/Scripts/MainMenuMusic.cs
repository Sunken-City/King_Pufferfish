using UnityEngine;
using System.Collections;

public class MainMenuMusic : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<AudioSource>().mute = GameController.isMusicMuted;
    }
}
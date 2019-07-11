using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    //Quit click handler
    public void onQuitClicked()
    {
        Debug.Log("Quit click");
        Application.Quit();
    }

    public void onPlayClicked()
    {
        Debug.Log("PLAY CLICKED");
    }

    public void onMusicSliderChange()
    {
        //Change Volume here
    }

    public void onVolumeSliderChange()
    {

    }
}
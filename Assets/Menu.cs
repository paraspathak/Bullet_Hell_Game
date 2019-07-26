using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject panel;

    private void Start()
    {
        openPanel();
    }

    public void openPanel()
    {
        if (panel != null)
        {
            Animator animator = panel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }

    //Quit click handler
    public void onQuitClicked()
    {
        Debug.Log("Quit click");
        Application.Quit();
    }

    public void onPlayClicked()
    {
        Debug.Log("PLAY CLICKED");
        //Load the next scene in play
        //Put the next scene as ship customization
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onMusicSliderChange()
    {
        //Change Volume here
    }

    public void onVolumeSliderChange()
    {

    }
}
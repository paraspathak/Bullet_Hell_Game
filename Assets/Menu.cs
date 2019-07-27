using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject panel;

    //Load the username here already
    public static string username;

    private void Start()
    {
        openPanel();
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //username = auth.CurrentUser.UserId;
        string user = auth.CurrentUser.UserId;
        DatabaseReference username_database = FirebaseDatabase.DefaultInstance.GetReference("users").Child(user);
        username_database.ValueChanged += Username_database_ValueChanged;

    }

    private void Username_database_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.Log(e.DatabaseError);
            return;
        }
        IDictionary dictionary = (IDictionary)e.Snapshot.Value;
        username = dictionary["username"].ToString();
        Debug.Log("username from database: " + username);
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
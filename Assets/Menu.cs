using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject panel;
    public Slider music_slider;
    public Slider volume_slider;

    //Load the username here already
    public static string username;

    private Firebase.Auth.FirebaseAuth auth;

    private void Start()
    {
        openPanel();
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
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
        save_credential();
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
        AudioListener.volume = music_slider.value;
    }

    public void onVolumeSliderChange()
    {
        AudioListener.volume = volume_slider.value;
    }

    public void onLogOutClick()
    {
        auth.SignOut();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1); //Load the previous screen
    }

    //Working
    public void save_credential()
    {
        if (authenticate_user.save)
        {
            string[] output = { "true", username, authenticate_user.email, authenticate_user.pasword};
            Debug.Log(output[1]);
            Debug.Log(output[2]);
            Debug.Log(output[3]);
            System.IO.File.WriteAllLines(Application.persistentDataPath + "/user.dat", output);
        }
        else
        {
            string[] output = { "false"};
            System.IO.File.WriteAllLines(Application.persistentDataPath + "/user.dat", output);
        }
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class authenticate_user : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_Text system_message;

    private Firebase.Auth.FirebaseAuth auth;

    private void Start()
    {
        auth = auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void AuthenticateUser()
    {
        string username =  usernameField.text;
        string password = passwordField.text;
        //Check if the fields are empty
        int count_error = 0;
        if (username.Length == 0) { count_error += 5; }
        if (password.Length == 0) { count_error += 2; }
        if (count_error == 2)
        {
            system_message.SetText("Empty Password!");
            Debug.Log("Empty username");
        }
        else if (count_error == 5)
        {
            system_message.SetText("Empty Username!");
            Debug.Log("Empty username");
        }
        else if (count_error == 7)
        {
            system_message.SetText("Empty Username and Password!");
            Debug.Log("Empty username");
        }
        else
        {
            if(!Validate_email(username))
            {
                system_message.SetText("Invalid Email Address!");
            }
            else
            {
                Debug.Log("Authenticate with Firebase here");
                login_userAsync(username, password);
            }
        }
        
    }

    public async System.Threading.Tasks.Task login_userAsync(string email, string password)
    {
        
        await auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                system_message.SetText("No Account found!");
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
                
            }
            else
            {
                //Doesnot seem to be sending incorrect password message to the user
                system_message.SetText(task.Exception.ToString());
            }            
        });
        load_next_scene();
    }
    
    //Clears the system message as user types in their address
    public void clear_system_message()
    {
        system_message.SetText("");
    }

    public static bool Validate_email(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public void load_next_scene()
    {
        Debug.Log("active scene is " + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

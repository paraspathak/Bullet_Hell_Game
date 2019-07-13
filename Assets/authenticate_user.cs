using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class authenticate_user : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_Text system_message;


    protected Firebase.Auth.FirebaseAuth auth;

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
            }
        }
        
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
}

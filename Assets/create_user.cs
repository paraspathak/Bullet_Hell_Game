using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class create_user : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_InputField emailField;
    public TMP_Text system_message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void create_account()
    {
        string email = emailField.text;
        string password = passwordField.text;
        string username = usernameField.text;
        bool is_username_valid = username.Length > 0;
        bool is_email_valid = authenticate_user.Validate_email(email);
        bool is_password_long = password.Length > 6;
        if (!is_email_valid)
        {
            system_message.SetText("Email is not valid");
        }
        if (!is_password_long)
        {
            system_message.SetText("Password must be 6 characters");
        }
        if (!is_username_valid)
        {
            system_message.SetText("Username is empty");
        }
        if (is_email_valid && is_password_long && is_username_valid)
        {
            Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    system_message.SetText("Cannot create an account with given credentials");
                    return;
                }
                else
                {
                    system_message.SetText("New Account created");
                }
                // Firebase user has been created.
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            });

        }
    }

    public void clear_system_message()
    {
        system_message.SetText("");
    }

    public void cancel_click()
    {
        usernameField.SetTextWithoutNotify("");
        passwordField.SetTextWithoutNotify("");
        emailField.SetTextWithoutNotify("");
    }

}

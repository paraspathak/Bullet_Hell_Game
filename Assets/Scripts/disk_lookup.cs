using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class disk_lookup : MonoBehaviour
{
    private float timer=5f;
    private int scene_number;
    private Firebase.Auth.FirebaseAuth auth;
    // Start is called before the first frame update
    void Start()
    {
        scene_number = read_data();
        Debug.Log(scene_number);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scene_number);
        }
    }

    private int read_data()
    {
        //FileStream fs = File.Create(Application.persistentDataPath + "/user");
        try
        {
            string[] lines = System.IO.File.ReadAllLines(Application.persistentDataPath + "/user.dat");
            Debug.Log(lines);
            if (lines[0].Equals("true"))
            {
                authenticate_user.save = true;  //Propagate the save
                Debug.Log("Username is saved, loading...");
                string username = lines[1];
                Debug.Log(username);
                string email = lines[2];
                string password = lines[3];
                Menu.username = username;
                //Authenticate with firebase here
                auth = auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                    if (task.IsCompleted)
                    {
                        Debug.Log("User signed in successfully");
                    }
                });
                return 2;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);   //Load the main menu
            }
            else
            {
                return 1;
                //Load the next scene
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //Load the login scene 
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
            FileStream fs = File.Create(Application.persistentDataPath + "/user.dat");
            return 1;
        }
       
    }
}

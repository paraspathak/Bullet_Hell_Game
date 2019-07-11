using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class authenticate_user : MonoBehaviour
{
    public GameObject create_user;
    public InputField inputField;


    // Start is called before the first frame update
    void Start()
    {
        create_user = GameObject.Find("CreateUserPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AuthenticateUser()
    {
        Debug.Log("Authenticate user here");
    }
}

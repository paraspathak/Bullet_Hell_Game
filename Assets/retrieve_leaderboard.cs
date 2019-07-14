using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Database
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Linq;

public class retrieve_leaderboard : MonoBehaviour
{
    public GameObject leaderboard_area;

    protected Dictionary<string, TMP_Text> map;
    protected DatabaseReference database;

    // Start is called before the first frame update
    void Start()
    {
        //Add elements onto the map for easy access for updating
        map = new Dictionary<string, TMP_Text>();
        TMP_Text[] table_entry = leaderboard_area.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text child in table_entry)
        {
            map.Add(child.name, child);
        }
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://bullet-hell-game.firebaseio.com/");
        database = FirebaseDatabase.DefaultInstance.RootReference;
        load_leaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load_leaderboard()
    {
        //Access the leaderboard region of the database        
        database.Child("leaderboard").GetValueAsync().ContinueWith(task=> {
            if (task.IsFaulted)
            {
                Debug.Log("Cannot access database");
                update_error();
            }
            else if (task.IsCompleted)
            {
                update_leaderboard(task.Result);
            }
        });
    }

    public void update_leaderboard(DataSnapshot snapshot)
    {
        foreach (DataSnapshot leaderboard_entry in snapshot.Children)
        {
            IDictionary dictUser = (IDictionary)leaderboard_entry.Value;
            Debug.Log("" + dictUser["username"] + " - " + dictUser["score"]);
        }

    }

    public void update_error()
    {

    }

}

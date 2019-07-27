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

    //To store the text reference to update with database entry
    protected List<TMP_Text> texts;
    protected List<TMP_Text> score;

    //Store the score retrieved from databse
    protected int player_score;
    protected string player_name;
    //Access the database entry
    //protected DatabaseReference database;

    // Start is called before the first frame update
    void Start()
    {
        player_score = GameController.score;
        player_name = Menu.username;
        //Initialization
        texts = new List<TMP_Text>();
        score = new List<TMP_Text>();
        //Access object
        TMP_Text[] table_entry = leaderboard_area.GetComponentsInChildren<TMP_Text>();
        //Store in different places
        foreach (TMP_Text child in table_entry)
        {
            if (child.name.Contains("username"))
            {
                texts.Add(child);
            }
            else
            {
                score.Add(child);
            }
        }
        score[3].SetText(player_score.ToString());
        texts[3].SetText(player_name);
        //Initialize the databse
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://bullet-hell-game.firebaseio.com/");
        DatabaseReference database = FirebaseDatabase.DefaultInstance.RootReference;
        database.Child("leaderboard").OrderByChild("score").ValueChanged += Retrieve_leaderboard_ValueChanged;    //Add a listener
    }

    private void Retrieve_leaderboard_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            update_error();
            return;
        }
        int counter = 0;    //Count for only first 4 entries
        foreach(DataSnapshot snapshot in e.Snapshot.Children)
        {
            if (counter > 3)
            {
                break;
            }
            IDictionary dictionary = (IDictionary)snapshot.Value;
            Debug.Log("username: " + dictionary["username"]);
            score[counter].SetText(dictionary["score"].ToString());
            texts[counter].SetText(dictionary["username"].ToString());
            counter++;
        }
    }
   
    public void update_error()
    {
        texts[0].SetText("Cannot connect with Database!");
    }

}
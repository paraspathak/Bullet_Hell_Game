using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text restartText;
    public TMP_Text gameOverText;
    public GameObject continue_button;

    //So as other classes can access it
    public static int score;

    private DatabaseReference leaderboard;
    private bool gameOver;
    private bool restart;
    private string UserName;

    private float delay = 5;    //Delay of time before the leaderboard pops open


    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = " ";
        gameOverText.text = " ";
        continue_button.SetActive(false);
        score = 0;
        UserName = Menu.username;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "GAME OVER";
        gameOver = true;

        //Show the hidden button
        continue_button.SetActive(true);

        Debug.Log("Game over");

        //Update the database here
        leaderboard = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
        leaderboard.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.Log("error accessing database");
            }
            else if (task.IsCompleted)
            {
                //<TODO> sometimes data gets repitition
                DataSnapshot e = task.Result;
                List<User> scoreboard = new List<User>();
                foreach (DataSnapshot dataSnapshot in e.Children)
                {
                    IDictionary dictionary = (IDictionary)dataSnapshot.Value;
                    int current_score = System.Int32.Parse(dictionary["score"].ToString());
                    scoreboard.Add(new User(dictionary["username"].ToString(), current_score));
                }
                scoreboard.Add(new User(Menu.username, score));
                scoreboard.Sort(delegate (User x, User y) {
                    return y.get_score().CompareTo(x.get_score());
                });
                Debug.Log("Scoreboard is sorted"+ scoreboard.Capacity);
                
                Dictionary<string, object> final = new Dictionary<string, object>();
                string previous_username = " ";
                int previous_score = -5;
                //final.Add("1", ToDictionary(scoreboard[0]));
                int counter = 1;
                foreach (var item in scoreboard)
                {
                    if (counter > 4)
                    {
                        break;
                    }
                    //Only add non repeated items in the dictionary
                    if (!(item.get_username().Equals(previous_username) && item.get_score() == previous_score))
                    {
                        Debug.Log(item.get_score() + " sorted " + item.get_username());
                        final.Add(counter.ToString(), ToDictionary(item));
                        previous_score = item.get_score();
                        previous_username = item.get_username();
                        counter++;
                    }
                }
                leaderboard.SetValueAsync(final);
            }
        });
        //leaderboard.ValueChanged += GameController_ValueChanged;
        
    }

    private void GameController_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        //int counter = 1;
        List<User> scoreboard = new List<User>();
        foreach (DataSnapshot dataSnapshot in e.Snapshot.Children)
        {
            IDictionary dictionary = (IDictionary)dataSnapshot.Value;
            int current_score = System.Int32.Parse(dictionary["score"].ToString());
            scoreboard.Add(new User(dictionary["username"].ToString(), current_score));
        }
        scoreboard.Add(new User(Menu.username, score));
        scoreboard.Sort(delegate (User x, User y) {
            return y.get_score().CompareTo(x.get_score());
        });
        Debug.Log("Scoreboard is sorted");
        Debug.Log("Last"+scoreboard[scoreboard.Capacity - 1].get_score());
        Debug.Log("First"+scoreboard[0].get_score());
        Dictionary<string, object> final = new Dictionary<string, object>();
        string previous_username = scoreboard[0].get_username();
        int previous_score= scoreboard[1].get_score();
        final.Add("1", ToDictionary(scoreboard[0]));
        int counter = 2;
        foreach( var item in scoreboard)
        {
            if (counter > 4)
            {
                break;
            }
            //Only add non repeated items in the dictionary
            if (!(item.get_username().Equals(previous_username) && item.get_score() == previous_score))
            {
                Debug.Log(item.get_score() + " sorted " + item.get_username());
                final.Add(counter.ToString(),ToDictionary(item));
                previous_score = item.get_score();
                previous_username = item.get_username();
                counter++;
            }            
        }
        leaderboard.SetValueAsync(final);

    }

    public Dictionary<string, string> ToDictionary(User user)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        //Get the username and update score here
        result["username"] = user.get_username();
        result["score"] = user.get_score().ToString();
        //Debug.Log("username from Menu is: " + result["username"] + "Score is : " + user.get_score());
        return result;
    }

    public void load_next_scene()
    {
        //Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void load_main_menu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1; //Game is paused so reload the game
    }
}

public class User
{
    private string username;
    private int score;
    public User(string name, int sc)
    {
        username = name;
        score = sc;
    }
    public string get_username()
    {
        return username;
    }
    public int get_score()
    {
        return score;
    }
    public static bool operator >(User a, User b)
    {
        return a.get_score() > b.get_score();
    }
    public static bool operator <(User a, User b)
    {
        return a.get_score() < b.get_score();
    }
}

using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Text gameOverText;
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
        leaderboard.ValueChanged += GameController_ValueChanged;
        /*
            FirebaseDatabase.DefaultInstance.GetReference("leaderboard").GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    //Handle error here
                }
                else if(task.IsCompleted)
                {
                    DatabaseReference leaderboard = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
                    DataSnapshot snapshot = task.Result;
                    int fourth = System.Int32.Parse(snapshot.Child("4").Child("score").Value.ToString());
                    if(score> fourth)
                    {
                        int third= System.Int32.Parse(snapshot.Child("3").Child("score").Value.ToString()); ;
                        if (score > third)
                        {
                            int second = System.Int32.Parse(snapshot.Child("2").Child("score").Value.ToString());
                            if (score > second)
                            {
                                int first = System.Int32.Parse(snapshot.Child("1").Child("score").Value.ToString());
                                if (score > 1)
                                {
                                    //Update first place
                                    leaderboard.Child("1").SetValueAsync(ToDictionary());
                                    Debug.Log("First");
                                }
                                else
                                {
                                    //update second place
                                    leaderboard.Child("2").SetValueAsync(ToDictionary());
                                    Debug.Log("Second");
                                }
                            }
                            else
                            {
                                //Update third place
                                leaderboard.Child("3").SetValueAsync(ToDictionary());
                                Debug.Log("Third");
                            }
                        }
                        else
                        {
                            //Update fourth place
                            leaderboard.Child("4").SetValueAsync(ToDictionary());
                            Debug.Log("Fourth");
                        }
                    }
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            });
        */
        //Load the next scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void GameController_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        int counter = 1;
        foreach (DataSnapshot dataSnapshot in e.Snapshot.Children)
        {
            IDictionary dictionary = (IDictionary)dataSnapshot.Value;
            int current_score = System.Int32.Parse(dictionary["score"].ToString());
            if (score > current_score)
            {
                //Update the entry here

                //<<TODO>> having issue here
                leaderboard.Child(counter.ToString()).SetValueAsync(ToDictionary());
                Debug.Log("Database is updated");
                return;
            }
            counter++;
        }
    }

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        //Get the username and update score here

        result["username"] = Menu.username;
        Debug.Log("username from Menu is: " + result["username"]);
        Debug.Log("username: " + UserName);
        result["score"] = score.ToString();

        return result;
    }

    public void load_next_scene()
    {
        //Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

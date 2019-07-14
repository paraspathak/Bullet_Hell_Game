using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class retrieve_leaderboard : MonoBehaviour
{
    public GameObject leaderboard_area;

    protected Dictionary<string, Text> map;

    // Start is called before the first frame update
    void Start()
    {
        //Add elements onto the map for easy access for updating
        map = new Dictionary<string, Text>();
        Text[] table_entry = leaderboard_area.GetComponentsInChildren<Text>();
        foreach (Text child in table_entry)
        {
            map.Add(child.name, child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load_leaderboard()
    {
        
    }
}

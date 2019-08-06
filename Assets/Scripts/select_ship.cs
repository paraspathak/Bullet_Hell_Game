using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class select_ship : MonoBehaviour
{
    public static int selected_ship;
    public TMP_Text system_message;
    public TMP_Text speed;
    public TMP_Text firerate;
    public Button first;
    public Button second;

    private Color signature_color;
    // Start is called before the first frame update
    void Start()
    {
        //Initially select first ship displayed
        selected_ship = 0;
        signature_color = new Color(0f, 255f, 255f, 255f);
        first_ship_selected();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void first_ship_selected()
    {
        selected_ship = 0;

        system_message.SetText("Aegis");
        first.image.color = signature_color;
        second.image.color = Color.grey;
        speed.SetText("Speed: 70");
        firerate.SetText("Fire Rate: 90");

    }
    public void second_ship_selected()
    {
        selected_ship = 1;

        system_message.SetText("Falken");
        second.image.color = signature_color;
        first.image.color = Color.grey;
        speed.SetText("Speed: 90");
        firerate.SetText("Fire Rate: 70");
    }

    public void on_play_click()
    {
        //Load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

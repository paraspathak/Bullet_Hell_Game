using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;

    public GameObject playerShipBlue;
    public GameObject playerShipWhite;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        if (select_ship.selected_ship == 1)
        {
            Instantiate(playerShipBlue, new Vector3(0,0,0), Quaternion.Euler(0,-90,0));
        }
        else
        {
            Instantiate(playerShipWhite, new Vector3(0,0,0), Quaternion.Euler(0,-90,0));
        }
    }


    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}

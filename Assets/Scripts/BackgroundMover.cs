using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public Rigidbody background;
    public float speed;

    void Start()
    {
        background.velocity  = transform.forward * speed;
    }
}

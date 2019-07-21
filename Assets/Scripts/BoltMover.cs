using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMover : MonoBehaviour
{
    public Rigidbody shot;
    public float speed;

    void Start()
    {
        shot.velocity  = transform.up * speed;
    }
}

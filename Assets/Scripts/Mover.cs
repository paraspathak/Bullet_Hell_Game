using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Rigidbody shot;
    public float speed;

    void Start()
    {
        shot.velocity  = transform.forward * speed;
    }
}

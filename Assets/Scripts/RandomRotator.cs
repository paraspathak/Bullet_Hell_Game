using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public Rigidbody pu;
    public float tumble;

    void Start()
    {
        pu.angularVelocity = Random.insideUnitSphere * tumble;
    }
}

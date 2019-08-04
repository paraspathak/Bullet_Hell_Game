using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Boundary boundary;
    public float speed;
    public float tilt;
    public float fireRate;
    private float nextFire;

    public GameObject shot;
    public Transform shotSpawn;

    public AudioSource explosion;

    void Start()
    {
        if (select_ship.selected_ship == 1)
        {
            //Faster but worse shooting ship
            speed = 30;
            fireRate = 0.40f;
        }
        else
        {
            //Slower but better shooting ship
            speed = 10;
            fireRate = 0.20f;
        }
    }

    void Update()
    {
        if(Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            explosion = GetComponent<AudioSource>();
            explosion.Play();
        }
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        float x = Input.acceleration.x;
        float z = Input.acceleration.y;

        Vector3 movement = new Vector3 (x, 0.0f, z);

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

    }
}

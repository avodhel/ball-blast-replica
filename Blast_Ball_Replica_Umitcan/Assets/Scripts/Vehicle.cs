using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("Speed")]
    [Range(0f, 10f)]
    public float vehicleSpeed = 3.3f;
    [Header("Stay on Screen")]
    public float maxX = 3.2f;
    public float minX = -3.3f;
    [Header("Shoot")]
    public Transform aim;
    public GameObject missile;

    float horizontal = 0f;

    Vector3 vec;
    Rigidbody2D physic;

    void Start()
    {
        getComponents();
    }

    void FixedUpdate()
    {
        vehicleMove();
        shoot();
    }

    void getComponents()
    {
        physic = GetComponent<Rigidbody2D>();
    }

    void vehicleMove()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        vec = new Vector3(horizontal * vehicleSpeed, physic.velocity.y, 0);
        physic.velocity = vec;

        //stay on screen field
        physic.position = new Vector3(
        Mathf.Clamp(physic.position.x, minX, maxX),
        transform.position.y
        );
    }

    void shoot()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(missile, aim.position, aim.rotation);
        }
    }
}

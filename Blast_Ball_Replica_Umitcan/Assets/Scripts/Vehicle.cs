using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("Speed")]
    public float vehicleSpeed = 3.3f;
    [Header("Stay on Screen")]
    public float maxX;
    public float minX;

    float horizontal = 0f;

    Vector3 vec;
    Rigidbody2D physic;

    void Start()
    {
        physic = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        vehicleMove();
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
}

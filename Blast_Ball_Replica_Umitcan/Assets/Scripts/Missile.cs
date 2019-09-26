using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("Speed")]
    [Range(0f, 50f)]
    public float missileSpeed = 10f;

    Rigidbody2D physic;

    void Start()
    {
        getComponents();
        force();
    }

    void getComponents()
    {
        physic = GetComponent<Rigidbody2D>();
    }

    void force()
    {
        physic.velocity = transform.up * missileSpeed;
    }
}

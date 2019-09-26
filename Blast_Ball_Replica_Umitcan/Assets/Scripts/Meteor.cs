using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Color[] colors;

    Rigidbody2D physic;
    SpriteRenderer sRenderer;

    void Start()
    {
        getComponents();
        chooseColor();
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, Random.Range(0f, 360f)));
    }

    void Update()
    {
        
    }

    void getComponents()
    {
        physic = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void chooseColor()
    {
        sRenderer.color = colors[Random.Range(0, colors.Length)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bounce !!!");
    }
}

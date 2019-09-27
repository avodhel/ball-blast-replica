using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Bounce")]
    [Range(0f, 25f)]
    public float bounceForce = 6f;
    [Header("Scale")]
    [Range(0.1f, 5f)]
    public float minScale = 0.5f;
    [Range(0.1f, 5f)]
    public float maxScale = 3f;
    [Header("Colors")]
    public Color[] colors;

    Rigidbody2D physic;
    SpriteRenderer sRenderer;

    bool bounceRight = false;
    bool bounceLeft = false;

    void Start()
    {
        getComponents();
        assignFeatures();
        throwMeteor();
    }

    private void Update()
    {
        rotateMeteor();
    }

    void getComponents()
    {
        physic = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void assignFeatures()
    {
        //random color
        sRenderer.color = colors[Random.Range(0, colors.Length)];
        //random rotation
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, Random.Range(0f, 360f)));
        //random scale
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void throwMeteor()
    {
        if (gameObject.transform.position.x < 0) //left side
        {
            this.physic.velocity = new Vector2(1f, -bounceForce / 2); //throw right
            bounceRight = true;
            bounceLeft = false;
        }
        if (gameObject.transform.position.x > 0) //right side
        {
            this.physic.velocity = new Vector2(-1f, -bounceForce / 2); //throw left
            bounceLeft = true;
            bounceRight = false;
        }
    }

    void rotateMeteor()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)) * Time.deltaTime);
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "groundTag")
    //    {
    //        if (throwRight)
    //        {
    //            this.physic.velocity = new Vector2(1f, throwSpeed); //throw right
    //        }
    //        else if (throwLeft)
    //        {
    //            this.physic.velocity = new Vector2(-1f, throwSpeed); //throw left
    //        }
    //    }
    //    if (col.gameObject.tag == "leftWallTag")
    //    {
    //        this.physic.velocity = new Vector2(1f, -throwSpeed / 2);
    //        throwRight = true;
    //        throwLeft = false;
    //    }
    //    if (col.gameObject.tag == "rightWallTag")
    //    {
    //        this.physic.velocity = new Vector2(-1f, -throwSpeed / 2);
    //        throwLeft = true;
    //        throwRight = false;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "groundTag") //when meteor hit the ground
        {
            if (bounceRight)
            {
                this.physic.velocity = new Vector2(1f, bounceForce); //bounce  to right
            }
            else if (bounceLeft)
            {
                this.physic.velocity = new Vector2(-1f, bounceForce); //bounce to left
            }
        }
        if (col.gameObject.tag == "leftWallTag") //when meteor hit the left wall
        {
            this.physic.velocity = new Vector2(1f, -bounceForce / 2); //bounce  to right
            bounceRight = true;
            bounceLeft = false;
        }
        if (col.gameObject.tag == "rightWallTag") //when meteor hit the right wall
        {
            this.physic.velocity = new Vector2(-1f, -bounceForce / 2); //bounce to left
            bounceLeft = true;
            bounceRight = false;
        }
    }

}

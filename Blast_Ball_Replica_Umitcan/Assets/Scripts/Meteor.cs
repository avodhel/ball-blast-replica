using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Throw")]
    [Range(0f, 25f)]
    public float throwSpeed = 6f;
    [Header("Scale")]
    [Range(0.1f, 5f)]
    public float minScale = 0.5f;
    [Range(0.1f, 5f)]
    public float maxScale = 3f;
    [Header("Colors")]
    public Color[] colors;

    Rigidbody2D physic;
    SpriteRenderer sRenderer;

    bool throwRight = false;
    bool throwLeft = false;

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
            this.physic.velocity = new Vector2(1f, -throwSpeed/2); //throw right
            throwRight = true;
            throwLeft = false;
        }
        if (gameObject.transform.position.x > 0) //right side
        {
            this.physic.velocity = new Vector2(-1f, -throwSpeed / 2); //throw left
            throwLeft = true;
            throwRight = false;
        }
    }

    void rotateMeteor()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)) * Time.deltaTime);
    }
    //-------------------------------------------------------------------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "groundTag")
        {
            if (throwRight)
            {
                this.physic.velocity = new Vector2(1f, throwSpeed); //throw right
            }
            else if (throwLeft)
            {
                this.physic.velocity = new Vector2(-1f, throwSpeed); //throw left
            }
        }
        if (col.gameObject.tag == "leftWallTag")
        {
            this.physic.velocity = new Vector2(1f, -throwSpeed / 2);
            throwRight = true;
            throwLeft = false;
        }
        if (col.gameObject.tag == "rightWallTag")
        {
            this.physic.velocity = new Vector2(-1f, -throwSpeed / 2);
            throwLeft = true;
            throwRight = false;
        }
    }

}

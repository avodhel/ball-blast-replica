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
    public float maxScale = 5f;
    [Header("Colors")]
    public Color[] colors;

    Rigidbody2D physic;
    SpriteRenderer sRenderer;

    [HideInInspector]
    public bool startControl = false;
    bool bounceRight = false;
    bool bounceLeft = false;

    void Start()
    {
        getComponents();
        assignFeatures();
        meteorActivate();
    }

    public void meteorActivate()
    {
        if (startControl)
        {
            physic.bodyType = RigidbodyType2D.Dynamic;

            bounceMeteor();
            bounceAccToScale();
        }
        else
        {
            physic.bodyType = RigidbodyType2D.Static;
        }
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

    void bounceMeteor()
    {
        if (gameObject.transform.position.x < 0) //when spawned left side
        {
            this.physic.velocity = new Vector2(1f, -bounceForce / 2); //bounce right
            bounceRight = true;
            bounceLeft = false;
        }
        if (gameObject.transform.position.x > 0) //when spawned right side
        {
            this.physic.velocity = new Vector2(-1f, -bounceForce / 2); //bounce left
            bounceLeft = true;
            bounceRight = false;
        }
    }

    void bounceAccToScale() //bounce according to scale
    {
        if (this.gameObject.transform.localScale.x > minScale && gameObject.transform.localScale.x <= 2f)
        {
            bounceForce = 5f;
        }
        else if (this.gameObject.transform.localScale.x > 2f && gameObject.transform.localScale.x <= 2.75f)
        {
            bounceForce = 6f;
        }
        else if (this.gameObject.transform.localScale.x > 2.75f && gameObject.transform.localScale.x <= 3.5f)
        {
            bounceForce = 7f;
        }
        else if (this.gameObject.transform.localScale.x > 3.5f && gameObject.transform.localScale.x <= 4.25f)
        {
            bounceForce = 8f;
        }
        else if (this.gameObject.transform.localScale.x > 4.25f && gameObject.transform.localScale.x <= maxScale)
        {
            bounceForce = 9f;
        }
    }

    void rotateMeteor()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)) * Time.deltaTime);
    }

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

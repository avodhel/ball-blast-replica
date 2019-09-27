using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meteor : MonoBehaviour
{
    [Header("Bounce")]
    [Range(0f, 25f)]
    public float bounceForce = 6f;
    [Header("Scale")]
    [Range(0.1f, 5f)]
    public float minScale = 1.5f;
    [Range(0.1f, 15f)]
    public float maxScale = 8f;
    [Header("Split")]
    public GameObject splitMeteor;
    [Header("Durability")]
    public Text durabilityText;
    [Range(0.1f, 100f)]
    public float durability = 1f;

    [HideInInspector]
    public Rigidbody2D physic;
    [HideInInspector]
    public SpriteRenderer sRenderer;
    [HideInInspector]
    public GameObject spawnControl;
    [HideInInspector]
    public bool bounceRight = false;
    [HideInInspector]
    public bool bounceLeft = false;

    public void Start()
    {
        getComponents();
    }

    public void Update()
    {
        rotateMeteor();
    }

    public void getComponents()
    {
        physic = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        spawnControl = GameObject.FindGameObjectWithTag("spawnControlTag");
    }

    void rotateMeteor()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)) * Time.deltaTime);
    }

    public void bounceAccToScale() //bounce according to scale
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

    public void OnTriggerEnter2D(Collider2D col)
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

    public void destroyOrSplit(GameObject parentMeteor, Vector3 scale, Vector3 pos, Quaternion rot, Color color)
    {
        Debug.Log("Scale: " + scale);
        Debug.Log("Position: " + pos);
        Debug.Log("Color: " + color);

        Destroy(parentMeteor);

        if (scale.x * 0.5f > minScale)
        {
            spawnControl.GetComponent<SpawnControl>().insSplitMeteor(parentMeteor, scale, pos, rot, color); //instance split meteors
        }
    }
}

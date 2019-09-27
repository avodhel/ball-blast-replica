using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedMeteor : Meteor
{
    [Header("Colors")]
    public Color[] colors;

    [HideInInspector]
    public bool startControl = false;

    private new void Start()
    {
        getComponents();
        assignFeatures();
        meteorActivate();
        metDurability("determine");
    }

    protected override void metDurability(string condition)
    {
        base.metDurability(condition);
        if (condition == "determine")
        {
            parentDur = randDur; //save parent's durability
        }
    }

    public void meteorActivate()
    {
        if (startControl) //meteor is ready for game
        {
            physic.bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;

            bounceMeteor();
            bounceAccToScale();
        }
        else //meteor is still on the road
        {
            physic.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
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
}

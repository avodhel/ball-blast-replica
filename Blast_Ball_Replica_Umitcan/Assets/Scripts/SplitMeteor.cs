using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitMeteor : Meteor
{
    private new void Start()
    {
        getComponents();
        bounceAccToScale();
    }

    public void bounceSplitMeteor(GameObject splitMet, string whichSide)
    {
        if (whichSide == "right")
        {
            splitMet.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, -bounceForce / 2); //bounce right
            bounceRight = true;
            bounceLeft = false;
        }
        if (whichSide == "left")
        {
            splitMet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, -bounceForce / 2); //bounce left
            bounceLeft = true;
            bounceRight = false;
        }
    }
}

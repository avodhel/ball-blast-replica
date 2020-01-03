using UnityEngine;

public class SplitMeteor : Meteor
{
    private new void Start()
    {
        GetComponents();
        BounceAccToScale();
        MeteorDurability("determine");
    }

    public void BounceSplitMeteor(GameObject splitMet, string whichSide)
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

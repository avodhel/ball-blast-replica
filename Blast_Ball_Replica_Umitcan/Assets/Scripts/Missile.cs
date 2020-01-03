using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("Speed")]
    [Range(0f, 50f)]
    public float missileSpeed = 10f;

    [Header("Damage")]
    [Range(0, 50)]
    public int missileDamage = 1;

    private Rigidbody2D rb2D;

    private void Start()
    {
        GetComponents();
        Force();
    }

    private void GetComponents()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Force()
    {
        rb2D.velocity = transform.up * missileSpeed; //apply force to missile
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "upLimitTag") //when missile reach to upper limit destroy it
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "meteorTag" ||
            col.gameObject.tag == "splitMeteorTag") //when missile hit the meteor
        {
            Destroy(gameObject); //after hit destroy missile
            FindObjectOfType<SoundControl>().PlaySound("Meteor Hit"); //play sound

            //script choose
            if (col.gameObject.tag == "meteorTag")
            {
                col.gameObject.GetComponent<SpawnedMeteor>().destroyOrSplit(col.gameObject,
                                                        col.gameObject.transform.localScale,
                                                        col.gameObject.transform.position,
                                                        col.gameObject.transform.rotation,
                                                        col.gameObject.GetComponent<SpriteRenderer>().color,
                                                        missileDamage);
            }
            else if (col.gameObject.tag == "splitMeteorTag")
            {
                col.gameObject.GetComponent<SplitMeteor>().destroyOrSplit(col.gameObject,
                                                      col.gameObject.transform.localScale,
                                                      col.gameObject.transform.position,
                                                      col.gameObject.transform.rotation,
                                                      col.gameObject.GetComponent<SpriteRenderer>().color,
                                                      missileDamage);
            }
        }
    }
}

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
        physic.velocity = transform.up * missileSpeed; //apply force to missile
    }

    void OnTriggerExit2D(Collider2D col)
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

            //script choose
            if (col.gameObject.tag == "meteorTag")
            {
                col.gameObject.GetComponent<SpawnedMeteor>().destroyOrSplit(col.gameObject,
                                                        col.gameObject.transform.localScale,
                                                        col.gameObject.transform.position,
                                                        col.gameObject.transform.rotation,
                                                        col.gameObject.GetComponent<SpriteRenderer>().color);
            }
            else if (col.gameObject.tag == "splitMeteorTag")
            {
                col.gameObject.GetComponent<SplitMeteor>().destroyOrSplit(col.gameObject,
                                                      col.gameObject.transform.localScale,
                                                      col.gameObject.transform.position,
                                                      col.gameObject.transform.rotation,
                                                      col.gameObject.GetComponent<SpriteRenderer>().color);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [Header("Spawn")]
    [Range(0f, 25f)]
    public float spawnTime = 10f;
    public GameObject meteor;
    [Header("Spawn Points")]
    public Transform leftSpawnPoint;
    public Transform leftTargetPoint;
    public Transform rightSpawnPoint;
    public Transform rightTargetPoint;

    GameObject meteorClone;

    private float nextSpawn = 0.0f;
    bool moveToTargetControl = true; //meteor moves to target point until reach it

    void Start()
    {
        getComponents();
    }

    void Update()
    {
        insMeteor();
    }

    void getComponents()
    {
        meteorClone = meteor.gameObject;
    }

    void insMeteor()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;
            meteorClone = Instantiate(meteor, leftSpawnPoint.position, leftSpawnPoint.rotation);
            moveToTargetControl = true;
        }

        if (moveToTargetControl)
        {
            moveToTarget();
        }
    }

    void moveToTarget()
    {
        float step = 2 * Time.deltaTime; // calculate distance to move
        meteorClone.transform.position = Vector3.MoveTowards(meteorClone.transform.position, leftTargetPoint.position, step);
        if (leftTargetPoint.position == meteorClone.transform.position)
        {
            moveToTargetControl = false;
            meteorClone.GetComponent<Meteor>().startControl = true;
            meteorClone.GetComponent<Meteor>().meteorActivate();
        }
    }
}

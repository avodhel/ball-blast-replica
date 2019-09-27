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
    Transform selectedTargetSide;
    Transform selectedSpawnSide;

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
        //default side
        selectedTargetSide = rightTargetPoint;
        selectedSpawnSide = rightSpawnPoint;
    }

    void insMeteor()
    {
        if (Time.time > nextSpawn)
        {
            //calculate spawn time
            nextSpawn = Time.time + spawnTime;
            //spawn side
            chooseSpawnSide();
            //instance meteor
            meteorClone = Instantiate(meteor, selectedSpawnSide.position, selectedSpawnSide.rotation);
            moveToTargetControl = true;
        }

        if (moveToTargetControl)
        {
            moveToTarget();
        }
    }

    void chooseSpawnSide()
    {
        float possibility = Random.Range(0f, 100f);
        if (possibility < 50f)
        {
            selectedSpawnSide = leftSpawnPoint;
            selectedTargetSide = leftTargetPoint;
        }
        else
        {
            selectedSpawnSide = rightSpawnPoint;
            selectedTargetSide = rightTargetPoint;
        }
    }

    void moveToTarget()
    {
        float step = 2 * Time.deltaTime; // calculate distance to move
        meteorClone.transform.position = Vector3.MoveTowards(meteorClone.transform.position, selectedTargetSide.position, step);
        if (selectedTargetSide.position == meteorClone.transform.position) //when meteor reach the target point
        {
            moveToTargetControl = false;
            meteorClone.GetComponent<Meteor>().startControl = true;
            meteorClone.GetComponent<Meteor>().meteorActivate();
        }
    }
}

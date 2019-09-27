using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [Header("Spawn Objects")]
    public GameObject meteor;
    [Header("Spawn Points")]
    public Transform leftSpawnPoint;
    public Transform leftTargetPoint;
    public Transform rightSpawnPoint;
    public Transform rightTargetPoint;

    GameObject insMeteor;

    bool moveToTargetControl = true;

    void Start()
    {
        insMeteor = Instantiate(meteor, leftSpawnPoint.position, leftSpawnPoint.rotation);
    }

    void Update()
    {
        if (moveToTargetControl)
        {
            moveToTarget();
        }
    }

    void moveToTarget()
    {
        float step = 2 * Time.deltaTime; // calculate distance to move
        insMeteor.transform.position = Vector3.MoveTowards(insMeteor.transform.position, leftTargetPoint.position, step);
        if (leftTargetPoint.position == insMeteor.transform.position)
        {
            moveToTargetControl = false;
            insMeteor.GetComponent<Meteor>().startControl = true;
            insMeteor.GetComponent<Meteor>().meteorActivate();
        }
    }
}

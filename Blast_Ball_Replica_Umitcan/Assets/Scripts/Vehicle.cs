using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("Speed")]
    [Range(0f, 10f)]
    public float vehicleSpeed = 3.3f;

    [Header("Stay on Screen")]
    public float maxX = 3.2f;
    public float minX = -3.3f;

    [Header("Shoot")]
    [Range(0f, 5f)]
    public float shootRate = 0.25f;
    public Transform aim;
    public GameObject missile;
    public GameObject missileContainer;

    private float nextShoot = 0.0f;
    float horizontal = 0f;
    bool vehicleControl = true; //vehicle move and shoot control

    GameObject gameControl;
    Vector3 vec;
    Rigidbody2D physic;

    void Start()
    {
        getComponents();
    }

    void FixedUpdate()
    {
        vehicleMove();
        shoot();
    }

    void getComponents()
    {
        physic = GetComponent<Rigidbody2D>();
        gameControl = GameObject.FindGameObjectWithTag("gameControlTag");
    }

    void vehicleMove()
    {
        if (vehicleControl)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            vec = new Vector3(horizontal * vehicleSpeed, physic.velocity.y, 0);
            physic.velocity = vec;
        }

        stayOnScreen();
    }

    void stayOnScreen() //stay on screen field
    {
        physic.position = new Vector3(
        Mathf.Clamp(physic.position.x, minX, maxX),
        transform.position.y
        );
    }

    void shoot()
    {
        if (Input.GetButton("Jump") && Time.time > nextShoot && vehicleControl)
        {
            nextShoot = Time.time + shootRate;
            (Instantiate(missile, aim.position, aim.rotation)as GameObject).transform.parent = missileContainer.transform;
            FindObjectOfType<SoundControl>().playSound("Shoot");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "meteorTag" || //when meteor hits vehicle
            col.gameObject.tag == "splitMeteorTag")
        {
            gameControl.GetComponent<GameControl>().gameOver(); //game over
            vehicleControl = false; //vehicle can't move or shoot
        }
    }
}

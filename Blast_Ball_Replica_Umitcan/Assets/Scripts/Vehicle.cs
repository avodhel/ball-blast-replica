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
    private float horizontal = 0f;
    private bool vehicleControl = true; //vehicle move and shoot control

    private GameObject gameControl;
    private Vector3 vec3;
    private Rigidbody2D rb2D;

    private void Start()
    {
        GetComponents();
    }

    private void FixedUpdate()
    {
        VehicleMove();
        Shoot();
    }

    private void GetComponents()
    {
        rb2D = GetComponent<Rigidbody2D>();
        gameControl = GameObject.FindGameObjectWithTag("gameControlTag");
    }

    private void VehicleMove()
    {
        if (vehicleControl)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            vec3 = new Vector3(horizontal * vehicleSpeed, rb2D.velocity.y, 0);
            rb2D.velocity = vec3;
        }

        StayOnScreen();
    }

    private void StayOnScreen()
    {
        rb2D.position = new Vector3(Mathf.Clamp(rb2D.position.x, minX, maxX),
                                    transform.position.y);
    }

    private void Shoot()
    {
        if (Input.GetButton("Jump") && Time.time > nextShoot && vehicleControl)
        {
            nextShoot = Time.time + shootRate;
            (Instantiate(missile, aim.position, aim.rotation)as GameObject).transform.parent = missileContainer.transform;
            FindObjectOfType<SoundControl>().PlaySound("Shoot");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "meteorTag" || //when meteor hits vehicle
            col.gameObject.tag == "splitMeteorTag")
        {
            gameControl.GetComponent<GameControl>().GameOver(); //game over
            vehicleControl = false; //vehicle can't move or shoot
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Meteor : MonoBehaviour
{
    [Header("Bounce")]
    [Range(0f, 25f)]
    public float bounceForce = 6f;

    [Header("Scale")]
    [Range(0.1f, 5f)]
    public float minScale = 1.5f;
    [Range(0.1f, 15f)]
    public float maxScale = 8f;

    [Header("Split")]
    public GameObject splitMeteor;

    [Header("Durability")]
    [Range(1, 100)]
    public int minDurability = 1;
    [Range(1, 100)]
    public int maxDurability = 3;
    public Text durabilityText;

    [HideInInspector]
    public Rigidbody2D physic;
    [HideInInspector]
    public SpriteRenderer sRenderer;
    [HideInInspector]
    public GameObject spawnControl;
    [HideInInspector]
    public GameObject gameControl;
    [HideInInspector]
    public bool bounceRight = false;
    [HideInInspector]
    public bool bounceLeft = false;
    [HideInInspector]
    public int randDur = 1;
    [HideInInspector]
    public int parentDur = 1;

    private int getDamage = 1;

    public void Start()
    {
        GetComponents();
        MeteorDurability("determine");
    }

    public void Update()
    {
        RotateMeteor();
    }

    protected void GetComponents()
    {
        physic = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        spawnControl = GameObject.FindGameObjectWithTag("spawnControlTag");
        gameControl = GameObject.FindGameObjectWithTag("gameControlTag");
    }

    protected virtual void MeteorDurability(string condition) //meteor durability
    {
        if (condition == "determine")
        {
            randDur = Random.Range(minDurability, maxDurability);
        }
        if (condition == "reduce") //reduce durability
        {
            randDur -= getDamage;
        }
        durabilityText.text = randDur.ToString();
    }

    protected void RotateMeteor()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)) * Time.deltaTime);
    }

    protected void BounceAccToScale() //bounce according to scale
    {
        if (this.gameObject.transform.localScale.x > minScale && gameObject.transform.localScale.x <= 2f)
        {
            bounceForce = 5f;
        }
        else if (this.gameObject.transform.localScale.x > 2f && gameObject.transform.localScale.x <= 2.75f)
        {
            bounceForce = 6f;
        }
        else if (this.gameObject.transform.localScale.x > 2.75f && gameObject.transform.localScale.x <= 3.5f)
        {
            bounceForce = 7f;
        }
        else if (this.gameObject.transform.localScale.x > 3.5f && gameObject.transform.localScale.x <= 4.25f)
        {
            bounceForce = 8f;
        }
        else if (this.gameObject.transform.localScale.x > 4.25f && gameObject.transform.localScale.x <= maxScale)
        {
            bounceForce = 9f;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "groundTag") //when meteor hit the ground
        {
            if (bounceRight)
            {
                this.physic.velocity = new Vector2(1f, bounceForce); //bounce  to right
            }
            else if (bounceLeft)
            {
                this.physic.velocity = new Vector2(-1f, bounceForce); //bounce to left
            }
            //play sound
            FindObjectOfType<SoundControl>().PlaySound("Meteor Ground");
        }
        if (col.gameObject.tag == "leftWallTag") //when meteor hit the left wall
        {
            this.physic.velocity = new Vector2(1f, -bounceForce / 2); //bounce  to right
            bounceRight = true;
            bounceLeft = false;
        }
        if (col.gameObject.tag == "rightWallTag") //when meteor hit the right wall
        {
            this.physic.velocity = new Vector2(-1f, -bounceForce / 2); //bounce to left
            bounceLeft = true;
            bounceRight = false;
        }
        if (col.gameObject.tag == "downLimitTag") //when meteor falls down
        {
            Destroy(gameObject);
        }
    }

    public void destroyOrSplit(GameObject parentMeteor, Vector3 scale, Vector3 pos, Quaternion rot, Color color, int damage)
    {
        getDamage = damage;
        MeteorDurability("reduce"); //reduce durability according to damage
        if (randDur <= 0) //when durability is 0
        {
            Destroy(parentMeteor); //destroy parent meteor
            gameControl.GetComponent<GameControl>().IncScore(10); //increase score

            //spawn split meteors
            if (scale.x * 0.5f > minScale)
            {
                spawnControl.GetComponent<SpawnControl>().InstanceSplitMeteor(scale, pos, rot, color, parentDur); //instance split meteors
            }
        }
    }
}

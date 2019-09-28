using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    [Header("Level Up")]
    [Range(0.01f, 1f)]
    public float lvlBarFillAmount = 0.1f;
    [Range(1, 10)]
    public int incMaxDurability = 2;
    [Range(1, 10)]
    public int incMinDurability = 1;
    public Text lvlUpText;
    [Header("Level Bar")]
    public Image lvlBarFilled;
    public Text currentLvlText;
    public Text nextLvlText;
    [Header("Level")]
    public Text lvlText;
    [Header("Spawn")]
    public GameObject spawnControl;
    [Header("Meteors")]
    public GameObject spawnedMeteor;
    public GameObject splitMeteor;
    public GameObject meteorContainer;
    [Header("Vehicle")]
    public GameObject vehicle;
    [Header("Missile")]
    public GameObject missile;

    bool noMeteorControl = false; //is there any meteor or not
    bool lvlBarFullControl = false; //turned true when level bar fulled

    int currentlvl = 1;
    int nextLvl = 2;

    void Start()
    {
        resetLvlValues();
        updateLvlValues();
    }

    void Update()
    {
        if (lvlBarFullControl) //when level bar fulled
        {
            checkOutMet(); // check out is there any meteor from previous level
        }
    }

    void resetLvlValues() //reset values after every restart game
    {
        spawnedMeteor.GetComponent<SpawnedMeteor>().maxDurability = 3;
        spawnedMeteor.GetComponent<SpawnedMeteor>().minDurability = 1;
        splitMeteor.GetComponent<SplitMeteor>().maxDurability = 2;
        splitMeteor.GetComponent<SplitMeteor>().minDurability = 1;
        missile.GetComponent<Missile>().missileDamage = 1;
    }

    void updateLvlValues() //update values after every level up and restart game
    {
        lvlBarFilled.fillAmount = 0; //reset level bar
        lvlBarFullControl = false; //level bar isn't full anymore
        currentLvlText.text = currentlvl.ToString();
        nextLvlText.text = nextLvl.ToString();
        lvlText.text = "Level: " + currentlvl;
    }

    public void fillLvlBar() //fill level bar after every meteor spawned
    {
        if (lvlBarFilled.fillAmount < 1)
        {
            lvlBarFilled.fillAmount += lvlBarFillAmount;

            if (lvlBarFilled.fillAmount >= 1)
            {
                spawnControl.GetComponent<SpawnControl>().insMetControl = false; //stop instance meteor
                Debug.Log("Meteor spawn stopped");
                lvlBarFull();
            }
        }
    }

    void lvlBarFull()
    {
        lvlBarFullControl = true;
        if (noMeteorControl) //if there is no meteor
        {
            lvlUp();
            spawnControl.GetComponent<SpawnControl>().insMetControl = true; //start instance meteor again
            Debug.Log("Meteor spawn started again");
            noMeteorControl = false; //there are meteors again
        }
    }

    void checkOutMet() //check out is there any meteor from last level
    {
        if (meteorContainer.transform.childCount == 0)
        {
            noMeteorControl = true; // there is no meteor
            lvlBarFull();
            Debug.Log("There is  no meteor");
        }
    }

    void lvlUp() //level up
    {
        // show level up text
        StartCoroutine(showTextForSeconds(lvlUpText, 2)); 

        //increase level bar values
        currentlvl += 1;
        nextLvl += 1;

        //update level values
        updateLvlValues();

        //reduce fill amount after every level up thus every next level will be more meteors
        if (lvlBarFillAmount > 0.01f)
        {
            lvlBarFillAmount -= 0.01f; 
        }

        //increase meteor's minimum durability 1 in 3 levels
        if (currentlvl % 3 == 0)
        {
            spawnedMeteor.GetComponent<SpawnedMeteor>().minDurability += incMinDurability;
        }

        // increase meteor's maximum durability 
        spawnedMeteor.GetComponent<SpawnedMeteor>().maxDurability += incMaxDurability;

        //reduce vehicle's shoot rate thus after every level up, it can be shoot faster
        //float vehicleShootRate = vehicle.GetComponent<Vehicle>().shootRate;
        //if (vehicleShootRate > 0.1f)
        //{
        //    vehicleShootRate -= 0.01f;
        //}

        //increase missile damage 1 in 5 levels
        if (currentlvl % 5 == 0)
        {
            missile.GetComponent<Missile>().missileDamage += 1;
        }
    }

    IEnumerator showTextForSeconds(Text text, float waitTime)
    {
        text.enabled = true;
        yield return new WaitForSeconds(waitTime);
        text.enabled = false;
    }
}

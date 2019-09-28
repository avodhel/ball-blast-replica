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
    public int incMaxDurability = 1;
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
    [Header("Vehicle")]
    public GameObject vehicle;

    int currentlvl = 1;
    int nextLvl = 2;

    void Start()
    {
        updateLvlValues();
    }

    void updateLvlValues()
    {
        lvlBarFilled.fillAmount = 0; //reset level bar
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
                lvlUp();
                Debug.Log("level up!!!");
            }
        }
    }

    void lvlUp() //level up
    {
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
        // increase meteor's maximum durability 
        spawnedMeteor.GetComponent<SpawnedMeteor>().maxDurability += incMaxDurability;
        //reduce vehicle's shoot rate thus after every level up, it can be shoot faster
        float vehicleShootRate = vehicle.GetComponent<Vehicle>().shootRate;
        if (vehicleShootRate > 0.1f)
        {
            vehicleShootRate -= 0.01f;
        }
    }
}

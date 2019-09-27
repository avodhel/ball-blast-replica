using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    [Header("Level Bar")]
    public Image lvlBarFilled;
    public Text currentLvlText;
    public Text nextLvlText;
    [Header("Spawn")]
    public GameObject spawnControl;
    [Header("Meteors")]
    public GameObject spawnedMeteor;
    public GameObject splitMeteor;

    int currentlvl = 1;
    int nextLvl = 2;

    void Start()
    {
        resetLvlValues();
    }

    void resetLvlValues()
    {
        lvlBarFilled.fillAmount = 0; //reset level bar
        currentLvlText.text = currentlvl.ToString();
        nextLvlText.text = nextLvl.ToString();
    }
}

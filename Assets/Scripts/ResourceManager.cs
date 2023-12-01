using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    private TextMeshProUGUI goldtxt;
    public int goldCount; //Total Gold Count

    //Structure Costs
    [Header("Structure Costs")]
    public int roadCost;
    public int houseCost;
    public int merchantCost;
    public int farmCost;

    void Start()
    {
        goldtxt = GameObject.Find("goldtxt").GetComponent<TextMeshProUGUI>();
        UpdateGold();
    }
    public void UpdateGold()
    {
        goldtxt.text = "Gold: " + goldCount;
    }
    public void StartDay() //Called in DayNightCycle
    {
        //Play any functions that trigger at the start of a day
    }
}

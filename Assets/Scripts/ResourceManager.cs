using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    private TextMeshProUGUI goldtxt;
    private TextMeshProUGUI foodtxt;
    private TextMeshProUGUI happytxt;
    public int goldCount; //Total Gold Count
    public int foodCount;
    public int happyCount;

    //Structure Costs
    [Header("Structure Costs")]
    public int roadCost;
    public int houseCost;
    public int merchantCost;
    public int farmCost;

    void Start()
    {
        goldtxt = GameObject.Find("goldtxt").GetComponent<TextMeshProUGUI>();
        foodtxt = GameObject.Find("foodtxt").GetComponent<TextMeshProUGUI>();
        happytxt = GameObject.Find("happytxt").GetComponent<TextMeshProUGUI>();
        UpdateGold();
        UpdateFood();
        UpdateHappy();
    }
    public void UpdateGold()
    {
        goldtxt.text = "Gold: " + goldCount;
    }
    public void UpdateFood()
    {
        foodtxt.text = "Food: " + foodCount;
    }
    public void UpdateHappy()
    {
        happytxt.text = "Happiness: " + happyCount + "%";
    }
    public void StartDay() //Called in DayNightCycle
    {
        //Play any functions that trigger at the start of a day
    }
}

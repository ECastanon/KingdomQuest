using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    private TextMeshProUGUI goldtxt;
    private TextMeshProUGUI foodtxt;
    private TextMeshProUGUI happytxt;
    private TextMeshProUGUI gpmtxt;
    private TextMeshProUGUI fpmtxt;
    public int goldCount; //Total Gold Count
    public int foodCount;
    public int happyCount;

    private int gpm = 1;
    private int fpm = 0;

    //Structure Costs
    [Header("Structure Costs")]
    public int roadCost;
    public int houseCost;
    public int merchantCost;
    public int farmCost;

    private float timer = 0f;
    public float delayAmount;
    public int houseCount;
    public int merchantCount;
    public int farmCount;
    public int specialCount;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        goldtxt = GameObject.Find("goldtxt").GetComponent<TextMeshProUGUI>();
        foodtxt = GameObject.Find("foodtxt").GetComponent<TextMeshProUGUI>();
        happytxt = GameObject.Find("happytxt").GetComponent<TextMeshProUGUI>();
        gpmtxt = GameObject.Find("gpmtxt").GetComponent<TextMeshProUGUI>();
        fpmtxt = GameObject.Find("fpmtxt").GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        UpdateGold();
        UpdateFood();
        UpdateHappy();
        timer += Time.deltaTime;

        if (timer >= delayAmount)
        {
            timer = 0f;
            foodCount += farmCount * 5;
            goldCount += merchantCount * 2;
            if (foodCount > 0) 
            {
            foodCount -= houseCount;
            }
            if(foodCount < 25)
            {
                happyCount--;
            }
            if(foodCount == 0)
            {
                happyCount -= 3;
            }
            if(foodCount > houseCount*20 && happyCount < 100)
            {
                happyCount++;
            }
        }
    }
    public void UpdateGold()
    {
        gpmtxt.text = "(+" + gpm*merchantCount*2 + ")";
        goldtxt.text = "Gold: " + goldCount;
    }
    public void UpdateFood()
    {
        if(fpm - houseCount + farmCount * 5 < 0)
        {
            fpmtxt.text = "(" + (fpm - houseCount + farmCount * 5) + ")";
        }
        else
        {
            fpmtxt.text = "(+" + (fpm - houseCount + farmCount * 5) + ")";
        }
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
    public void incrementHouse()
    {
        houseCount++;
    }

}

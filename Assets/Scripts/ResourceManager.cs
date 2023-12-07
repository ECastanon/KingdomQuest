using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    private TextMeshProUGUI goldtxt, foodtxt, happytxt, healthtxt; //Text Objects
    private NotificationPopup notif;
    private Image foodBar, healthBar, happyBar;
    private PeopleManager peopleManager;

    [Header("Town Resources")]
    public int goldCount; //Total Gold Count
    private int gpm = 1;
    public float foodCount;
    public float happyCount;
    public float healthValue;

    //Structure Costs
    [Header("Structure Costs")]
    public int roadCost;
    public int houseCost;
    public int merchantCost;
    public int farmCost;
    public int hospitalCost;
    [Header("Structure Variables")]
    public int merchantProduction = 10; //Money a single merchant will produce
    public int farmProduction = 5; //Number of people a single farm can supply
    public int hospitalEffectiveness = 5; //Number of people a hospital can supply

    [Header("Structure Counts")]
    public int houseCount;
    public int merchantCount;
    public float farmCount;
    public int specialCount;
    public float hospitalCount;
    
    [Header("Timers")]
    public float timer = 0f;
    public float delayAmount = 5;
    public float notifTimer = 0f;
    public float notifDelayAmount = 20f;
    public float dayTimer = 0;
    public float dayDelayAmount = 60f;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        goldtxt = GameObject.Find("goldtxt").GetComponent<TextMeshProUGUI>();
        foodtxt = GameObject.Find("foodtxt").GetComponent<TextMeshProUGUI>();
        healthtxt = GameObject.Find("healthtxt").GetComponent<TextMeshProUGUI>();
        happytxt = GameObject.Find("happytxt").GetComponent<TextMeshProUGUI>();
        peopleManager = GameObject.Find("PeopleManager").GetComponent<PeopleManager>();
        notif = GameObject.Find("NotificationPanel ").GetComponent<NotificationPopup>();

        //Get Bars
        foodBar = GameObject.Find("FoodBar").GetComponent<Image>();
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        happyBar = GameObject.Find("HappyBar").GetComponent<Image>();
        foodBar.fillAmount = 0; healthBar.fillAmount = 0; happyBar.fillAmount = 0;

    }
    void Update()
    {
        UpdateGold();
        UpdateFood();
        UpdateHealth();
        UpdateHappy();
        if(timer > delayAmount)
        {
            timer = 0;
            gpm = merchantCount * merchantProduction;
            goldCount += gpm;
        } else {timer += Time.deltaTime;}
        if(notifTimer > notifDelayAmount)
        {
            notifTimer = 0;
            if(foodCount <= 60)
            {
                notif.SlideInNotif("Food Reserves running low! Build more Farms!");
            }
            if(healthValue <= 60)
            {
                notif.SlideInNotif("Citizens need healthcare! Build more Hospitals!");
            }
            if(happyCount <= 50)
            {
                notif.SlideInNotif("Your citizens are unhappy and threatening to leave!");
            }
        } else {notifTimer += Time.deltaTime;}
        if(dayTimer > dayDelayAmount)
        {
            dayTimer = 0;
            StartDay();
        } else {dayTimer += Time.deltaTime;}
    }
    private int NumOfCitizens()
    {
        return peopleManager.transform.childCount;
    }
    public void UpdateGold()
    {
        goldtxt.text = "Gold: " + goldCount + " (+" + gpm + ")";
    }
    public void UpdateFood()
    {
        if(NumOfCitizens() > 0)
        {
            foodCount = ((farmCount * farmProduction) / NumOfCitizens()); //Converts into a percentage
            foodBar.fillAmount = foodCount;
            foodCount *= 100;
            if(foodCount > 100){foodCount = 100;}
            foodtxt.text = Mathf.RoundToInt(foodCount) + "%";
        } else {foodtxt.text = "";}
    }
    private void UpdateHealth()
    {
        if(NumOfCitizens() > 0)
        {
            healthValue = ((hospitalCount * hospitalEffectiveness) / NumOfCitizens()); //Converts into a percentage
            healthBar.fillAmount = healthValue;
            healthValue *= 100;
            if(healthValue > 100){healthValue = 100;}
            healthtxt.text = Mathf.RoundToInt(healthValue) + "%";
        } else {healthtxt.text = "";}
    }
    public void UpdateHappy()
    {
        if(NumOfCitizens() > 0)
        {
            happyCount = ((foodCount + healthValue) / 200);
            happyBar.fillAmount = happyCount;
            happyCount *= 100;
            happytxt.text = Mathf.RoundToInt(happyCount) + "%";
        } else {happytxt.text = "";}
    }
    public void StartDay()
    {
        //Play any functions that trigger at the start of a day
        if(NumOfCitizens() > 5)
        {
            peopleManager.PeopleEnter();
            peopleManager.PeopleLeave();
        }
    }
    public void incrementHouse()
    {
        houseCount++;
    }
}

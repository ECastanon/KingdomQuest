using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PeopleManager : MonoBehaviour
{
    private TextMeshProUGUI peopletxt;
    private ResourceManager rm;
    private NotificationPopup notif;
    public int population = 0;
    public List<GameObject> citizenPrefabs;
    public List<GameObject> peopleOnMap;
    // Start is called before the first frame update
    void Start()
    {
        peopletxt = GameObject.Find("peopletxt").GetComponent<TextMeshProUGUI>();
        rm = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        notif = GameObject.Find("NotificationPanel ").GetComponent<NotificationPopup>();
        UpdatePeople();
    }
    void Update()
    {
        UpdatePeople();
    }

    // Update is called once per frame
    public void UpdatePeople()
    {
        peopletxt.text = "Population: " + population; 
    }
    public void PeopleLeave()
    {
        //When happiness is low, get a number of cycles to loop through based on population
        //then removes people by that number
        int cycles = 0, rand;
        if(rm.happyCount <= 50)
        {
            if(population >= 30){cycles = 10;}
            if(population >= 20){cycles = 8;}
            if(population >= 10){cycles = 4;}
            if(population >= 5){cycles = 2;}
        }
        if(cycles != 0)
        {
            for (int i = 0; i < cycles; i++)
            {
                rand = Random.Range(0, peopleOnMap.Count);
                Destroy(peopleOnMap[rand]);
                peopleOnMap.Remove(peopleOnMap[rand]);
                population -= 1;
            }
            notif.SlideInNotif(cycles + " have left the city!!!");
        }
    }
    public void PeopleEnter()
    {
        //When happiness is high, get a number of cycles to loop through based on house count
        //then increase people by that number
        int cycles = 0, rand;
        if(rm.happyCount >= 80)
        {
            rand = Random.Range(1, rm.houseCount);
            cycles = rand;
            if(cycles != 0)
            {
                notif.SlideInNotif(cycles + "people have entered the city!!!");
            }
            while(cycles > 0)
            {
                int rand_ = Random.Range(0, citizenPrefabs.Count);
                GameObject civ = Instantiate(citizenPrefabs[rand_], transform);
                population += 1; //Increases population when spawning a person
                civ.transform.SetParent(transform);
                peopleOnMap.Add(civ);
                cycles -= 1;
                FindRandomPersonLocation(civ);
            }
        }
    }
    private void FindRandomPersonLocation(GameObject civ) //Creates a list of straight/deadend roads and randomly places a person on one
    {
        List<GameObject> straights = new List<GameObject>();
        foreach (var road in GameObject.Find("RoadManager").GetComponent<RoadManager>().roadList)
        {
            if(road.transform.name == "StraightRoad(Clone)" || road.transform.name == "DeadEndRoad(Clone)")
            {
                straights.Add(road);
            }
        }
        int rand = Random.Range(0, straights.Count);
        civ.transform.position = new Vector3(straights[rand].transform.position.x, 0.02f, straights[rand].transform.position.z);
        civ.transform.rotation = Quaternion.Euler(0, straights[rand].transform.localRotation.eulerAngles.y, 0);
    }
}

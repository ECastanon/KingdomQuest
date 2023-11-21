using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PeopleManager : MonoBehaviour
{
    private TextMeshProUGUI peopletxt; 
    public int population = 0;
    // Start is called before the first frame update
    void Start()
    {
        peopletxt = GameObject.Find("peopletxt").GetComponent<TextMeshProUGUI>();
        UpdatePeople();

        
    }

    // Update is called once per frame
    public void UpdatePeople()
    {
        peopletxt.text = "Population: " + population; 
    }
}

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

    void Start()
    {
        goldtxt = GameObject.Find("goldtxt").GetComponent<TextMeshProUGUI>();
    }
    public void UpdateGold()
    {
        goldtxt.text = "Gold: " + goldCount;
    }
}

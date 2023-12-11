using SVS;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    private PlacementManager pm;
    private UIController uc;
    public List<GameObject> housePrefabs;
    public List<GameObject> specialPrefabs;
    public List<GameObject> merchantPrefabs;
    public List<GameObject> farmPrefabs;
    public List<GameObject> hospitalPrefabs;

    private void Start()
    {
        pm = GameObject.Find("PlacementManager").GetComponent<PlacementManager>();
        uc = GameObject.Find("Canvas").GetComponent<UIController>();
    }
    public void PlaceHouse(Vector3Int position) //Passes house information to PlacementManager
    {
        List<GameObject> straights = new List<GameObject>();
        foreach (var road in GameObject.Find("RoadManager").GetComponent<RoadManager>().roadList)
        {
            if (road.transform.name == "StraightRoad(Clone)" || road.transform.name == "DeadEndRoad(Clone)")
            {
                straights.Add(road);
            }
        }
        if (straights.Count >= 2)
        {
            if (CheckPositionBeforePlacement(position))
            {
                int house = GameObject.Find("HouseMenu").GetComponent<HousePanel>().houseToUse;
                if(house > 0)
                {
                    pm.PlaceOnMap(position, housePrefabs[house-1], CellType.Structure);
                    AudioPlayer.instance.PlayPlacementSound();
                } else {
                    uc.ShowWarning("First select a house type!");
                }
            }
            Debug.Log("House Pos: " + position);
        }
        else
        {
            uc.ShowWarning("You must have at least 2 roads before building a house!");
        }
    }

    public void PlaceSpecial(Vector3Int position) //Passes special information to PlacementManager
    {
        if (CheckPositionBeforePlacement(position))
        {
            int rand = Random.Range(0, specialPrefabs.Count);
            pm.PlaceOnMap(position, specialPrefabs[rand], CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }
    public void PlaceMerchant(Vector3Int position) //Passes merchant information to PlacementManager
    {
        if (CheckPositionBeforePlacement(position))
        {
            int merch = GameObject.Find("MerchantMenu").GetComponent<HousePanel>().houseToUse;
            if(merch > 0)
            {
                pm.PlaceOnMap(position, merchantPrefabs[merch-1], CellType.merchantStructure);
                AudioPlayer.instance.PlayPlacementSound();
            } else {
                uc.ShowWarning("First select a merchant type!");
            }
        }
    }
    public void PlaceFarm(Vector3Int position) //Passes farm information to PlacementManager
    {
        if (CheckPositionBeforePlacement(position))
        {
            int farm = GameObject.Find("FarmMenu").GetComponent<HousePanel>().houseToUse;
            if(farm > 0)
            {
                pm.PlaceOnMap(position, farmPrefabs[farm-1], CellType.farmStructure);
                AudioPlayer.instance.PlayPlacementSound();
            } else {
                uc.ShowWarning("First select a farm type!");
            }
        }
    }
    public void PlaceHospital(Vector3Int position) //Passes hospital information to PlacementManager
    {
        if (CheckPositionBeforePlacement(position))
        {
            int rand = Random.Range(0, hospitalPrefabs.Count);
            pm.PlaceOnMap(position, hospitalPrefabs[rand], CellType.hospitalStructure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }
    private bool CheckPositionBeforePlacement(Vector3Int position)
    {
        if (pm.CheckIfPositionInBound(position) == false)
        {
            Debug.Log("This position is out of bounds");
            return false;
        }
        if (pm.CheckIfPositionIsFree(position) == false)
        {
            Debug.Log("This position is not EMPTY");
            return false;
        }
        return CheckIfMountainNear(position);
        return true;
    }
    private bool CheckIfMountainNear(Vector3Int position) //Prevents structure placement if too close to a mountain
    {
        RaycastHit[] hits = Physics.BoxCastAll(position + new Vector3(0, 0.5f, 0), new Vector3(0.5f, 0.5f, 0.5f), transform.up, Quaternion.identity, 1f, 1 << LayerMask.NameToLayer("Mountain"));
        if(hits.Length > 0)
        {
            Debug.Log("This position is too close to a mountain!");
            return false;
        } else {return true;}
    }
}
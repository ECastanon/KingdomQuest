using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureModel : MonoBehaviour
{
    float yHeight = 0;
    public Vector3Int RoadPos {get; set;}
    public int roadListID = 0;
    public void CreateModel(GameObject model, CellType type) //Loads the object passed in
    {
        var structure = Instantiate(model, transform);
        if(type == CellType.Road)
        {
            GameObject.Find("RoadManager").GetComponent<RoadManager>().roadList.Add(structure);
            roadListID = GameObject.Find("RoadManager").GetComponent<RoadManager>().roadList.Count-1;
        }
        yHeight = structure.transform.position.y;
        structure.transform.localPosition = new Vector3(0,yHeight,0);
        checkHouse(model);
    }
    public void SwapModel(GameObject model, Quaternion rotation, CellType type) //Changes model
    {
        GameObject.Find("RoadManager").GetComponent<RoadManager>().roadList[roadListID] = null;
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var structure = Instantiate(model, transform);

        if(type == CellType.Road)
        {
            GameObject.Find("RoadManager").GetComponent<RoadManager>().roadList[roadListID] = structure;
        }
        structure.transform.localPosition = new Vector3(0,yHeight,0);
        structure.transform.localRotation = rotation;
    }
    public void checkHouse(GameObject obj)
    {
        if (obj.tag == "house")
        {

            int rand = Random.Range(0, GameObject.Find("PeopleList").GetComponent<PeopleList>().citizenPrefabs.Count);
            GameObject civ = Instantiate(GameObject.Find("PeopleList").GetComponent<PeopleList>().citizenPrefabs[rand], transform);
            civ.transform.SetParent(GameObject.Find("PeopleList").transform);
            FindRandomPersonLocation(civ);
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
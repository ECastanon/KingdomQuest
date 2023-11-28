using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureModel : MonoBehaviour, INeedingRoad
{
    float yHeight = 0;
    public Vector3Int RoadPos {get; set;}
    public void CreateModel(GameObject model) //Loads the object passed in
    {
        var structure = Instantiate(model, transform);
        yHeight = structure.transform.position.y;
        structure.transform.localPosition = new Vector3(0,yHeight,0);
    }
    public void SwapModel(GameObject model, Quaternion rotation) //Changes model
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0,yHeight,0);
        structure.transform.localRotation = rotation;
    }
}

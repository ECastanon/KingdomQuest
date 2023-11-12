using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    Grid placementGrid;
    private Dictionary<Vector3Int, StructureModel> tempRoadObject = new Dictionary<Vector3Int, StructureModel>();
    private Dictionary<Vector3Int, StructureModel> structDict = new Dictionary<Vector3Int, StructureModel>(); 
    private ResourceManager rm;

    void Start()
    {
        rm = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        placementGrid = new Grid(width,height);
    }
    public bool CheckIfPositionInBound(Vector3Int pos) //Checks if the selected tile is within the given bounds
    {
        if(pos.x >= 0 && pos.x < width && pos.x >= 0 && pos.z < height && pos.y == 0)
        {
            return true;
        }
        return false;
    }
    public bool CheckIfPositionIsFree(Vector3Int pos) //Does not place an object if tile is taken
    {
        return CheckIfPositionOfType(pos, CellType.Empty);
    }
    public void PlaceTempStructure(Vector3Int pos, GameObject structPrefab, CellType type) //Passes information to CreateNewSructureModel
    {
        if(rm.goldCount >= rm.roadCost) //Change cost to be either house or road depending on prefab type
        {
            placementGrid[pos.x, pos.z] = type;
            StructureModel structure = CreateNewStructureModel(pos, structPrefab, type);
            tempRoadObject.Add(pos, structure);
            rm.goldCount -= rm.roadCost;
        }
    }
    private bool CheckIfPositionOfType(Vector3Int pos, CellType type)
    {
        return placementGrid[pos.x, pos.z] == type;
    }
    private StructureModel CreateNewStructureModel(Vector3Int pos, GameObject structPrefab, CellType type) //Creates the given object
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = pos;
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structPrefab);
        return structureModel;
    }
    public void ModifyStructureModel(Vector3Int pos, GameObject newModel, Quaternion rotation) //Changes object model and rotation
    {
        if(tempRoadObject.ContainsKey(pos))
        {
            tempRoadObject[pos].SwapModel(newModel,rotation);
        } else if(structDict.ContainsKey(pos))
        {
            structDict[pos].SwapModel(newModel,rotation);
        }
    }
    public CellType[] GetNeighborTypesFor(Vector3Int pos) //Returns array of tile neighbors
    {
        return placementGrid.GetAllAdjacentCellTypes(pos.x, pos.z);
    }
    public List<Vector3Int> GetNeighborsOfTypeFor(Vector3Int pos, CellType type) //Returns the neighbors' Cell Type
    {
        var neighborVertices = placementGrid.GetAdjacentCellsOfType(pos.x, pos.z, type);
        List<Vector3Int> neighbors = new List<Vector3Int>();
        foreach(var point in neighborVertices)
        {
            neighbors.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return neighbors;
    }
    public List<Vector3Int> GetPathBetween(Vector3Int startPos, Vector3Int endPos)//Finds shortest distance when drawing roads
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPos.x, startPos.z), new Point(endPos.x, endPos.z));
        List<Vector3Int> path = new List<Vector3Int>();
        foreach(Point point in resultPath)
        {
            path.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return path;
    }
    public void RemoveAllTempStructures()//Removes structures when replacing with A* pathfinding
    {
        foreach(var struc in tempRoadObject.Values)
        {
            var pos = Vector3Int.RoundToInt(struc.transform.position);
            placementGrid[pos.x, pos.z] = CellType.Empty;
            Destroy(struc.gameObject);
        }
        tempRoadObject.Clear();
    }
    public void AddToStructDictionary()
    {
        foreach(var struc in tempRoadObject)
        {
            structDict.Add(struc.Key, struc.Value);
        }
        tempRoadObject.Clear();
    }
}

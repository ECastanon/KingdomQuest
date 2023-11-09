using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    Grid placementGrid;
    private Dictionary<Vector3Int, StructureModel> tempRoadObject = new Dictionary<Vector3Int, StructureModel>();
    private ResourceManager rm;

    void Start()
    {
        rm = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        placementGrid = new Grid(width,height);
    }
    public bool CheckIfPositionInBound(Vector3Int pos)
    {
        if(pos.x >= 0 && pos.x < width && pos.x >= 0 && pos.z < height && pos.y == 0)
        {
            return true;
        }
        return false;
    }
    public bool CheckIfPositionIsFree(Vector3Int pos)
    {
        return CheckIfPositionOfType(pos, CellType.Empty);
    }
    public void PlaceTempStructure(Vector3Int pos, GameObject structPrefab, CellType type)
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
    private StructureModel CreateNewStructureModel(Vector3Int pos, GameObject structPrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = pos;
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structPrefab);
        return structureModel;
    }
    public void ModifyStructureModel(Vector3Int pos, GameObject newModel, Quaternion rotation)
    {
        if(tempRoadObject.ContainsKey(pos))
        {
            tempRoadObject[pos].SwapModel(newModel,rotation);
        }
    }
    public CellType[] GetNeighborTypesFor(Vector3Int pos)
    {
        return placementGrid.GetAllAdjacentCellTypes(pos.x, pos.z);
    }
    public List<Vector3Int> GetNeighborsOfTypeFor(Vector3Int pos, CellType type)
    {
        var neighborVertices = placementGrid.GetAdjacentCellsOfType(pos.x, pos.z, type);
        List<Vector3Int> neighbors = new List<Vector3Int>();
        foreach(var point in neighborVertices)
        {
            neighbors.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return neighbors;
    }
}

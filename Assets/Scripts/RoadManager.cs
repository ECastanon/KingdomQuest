using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager pm;
    public List<Vector3Int> temporaryPlacementPos = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToReCheck = new List<Vector3Int>();
    public GameObject roadStraight;
    public RoadFixer roadFixer;

    void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    public void PlaceRoad(Vector3Int pos)
    {
        if(pm.CheckIfPositionInBound(pos) == false)
        {
            return;
        }
        if(pm.CheckIfPositionIsFree(pos) == false)
        {
            return;
        }
        temporaryPlacementPos.Clear();
        temporaryPlacementPos.Add(pos);  
        pm.PlaceTempStructure(pos, roadStraight, CellType.Road);

        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach(var tempPos in temporaryPlacementPos)
        {
            roadFixer.FixRoadAtPosition(pm, tempPos);
            var neighbors = pm.GetNeighborsOfTypeFor(tempPos, CellType.Road);
            foreach(var roadPos in neighbors)
            {
                roadPositionsToReCheck.Add(roadPos);
            }
        }
        foreach(var positionsToFix in roadPositionsToReCheck)
        {
            roadFixer.FixRoadAtPosition(pm, positionsToFix);
        }
    }
}

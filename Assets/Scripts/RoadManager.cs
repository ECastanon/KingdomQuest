using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    private PlacementManager pm;
    public List<Vector3Int> temporaryPlacementPos = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToReCheck = new List<Vector3Int>();
    private RoadFixer roadFixer;
    private Vector3Int startPos;
    public bool placementMode = false;
    public List<GameObject> roadList = new List<GameObject>();

    void Start()
    {
        pm = GameObject.Find("PlacementManager").GetComponent<PlacementManager>();
        roadFixer = GetComponent<RoadFixer>();
    }

    public void PlaceRoad(Vector3Int pos) //Passes road information to PlacementManager
    {
        if(pm.CheckIfPositionInBound(pos) == false){return;}
        if(pm.CheckIfPositionIsFree(pos) == false){return;}
        if(placementMode == false)
        {
            temporaryPlacementPos.Clear();
            roadPositionsToReCheck.Clear();

            placementMode = true;
            startPos = pos;

            temporaryPlacementPos.Add(pos);  
            if(CheckIfMountainNear(pos)){pm.PlaceOnMap(pos, roadFixer.deadEnd, CellType.Road);}
        } else if (placementMode == true)
        {
            pm.RemoveAllTempStructures();
            temporaryPlacementPos.Clear();

            foreach(var posToFix in roadPositionsToReCheck)
            {
                roadFixer.FixRoadAtPosition(pm, posToFix);
            }
            roadPositionsToReCheck.Clear();

            Debug.Log(startPos + "--" + pos);
            temporaryPlacementPos = pm.GetPathBetween(startPos, pos);

            foreach(var tempPos in temporaryPlacementPos)
            {
                if(pm.CheckIfPositionIsFree(pos) == false){continue;}
                if(CheckIfMountainNear(tempPos)){pm.PlaceOnMap(tempPos, roadFixer.deadEnd, CellType.Road);}
            }
        }
        FixRoadPrefabs();
    }
    private void FixRoadPrefabs() //Fixes road type from RoadFixer
    {
        foreach(var tempPos in temporaryPlacementPos)
        {
            roadFixer.FixRoadAtPosition(pm, tempPos);
            var neighbors = pm.GetNeighborsOfTypeFor(tempPos, CellType.Road);
            foreach(var roadPos in neighbors)
            {
                if(roadPositionsToReCheck.Contains(roadPos) == false)
                {
                    roadPositionsToReCheck.Add(roadPos);
                }
            }
        }
        foreach(var posToFix in roadPositionsToReCheck)
        {
            roadFixer.FixRoadAtPosition(pm, posToFix);
        }
    }
    public void FinishPlacingRoad()
    {
        placementMode = false;
        pm.AddToStructDictionary();
        if(temporaryPlacementPos.Count > 0)
        {
            //AudioPlayer.instance.PlayPlacementSound();
        }
        temporaryPlacementPos.Clear();
        startPos = Vector3Int.zero;
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
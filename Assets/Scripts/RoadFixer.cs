using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadFixer : MonoBehaviour
{
    public GameObject deadEnd, roadStraight, corner, threeWay, fourWay;
    
    public void FixRoadAtPosition(PlacementManager pm, Vector3Int tempPos)
    {
        //right, up, left, down - order checked
        var result = pm.GetNeighborTypesFor(tempPos);
        int roadCount = 0;
        roadCount = result.Where(x => x == CellType.Road).Count();
        if(roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(pm, result, tempPos);
        } else if(roadCount == 2)
        {
            if(CreateStraightRoad(pm, result, tempPos))
            {
                return;
            }
            CreateCorner(pm, result, tempPos);
        } else if(roadCount == 3)
        {
            Create3Way(pm, result, tempPos);
        } else
        {
            Create4Way(pm, result, tempPos);
        }
    }
    private void CreateDeadEnd(PlacementManager pm, CellType[] result, Vector3Int pos)
    {
        if(result[1] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, deadEnd, Quaternion.Euler(0,270,0));
        } else if(result[2] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, deadEnd, Quaternion.Euler(0,0,0));
        } else if(result[3] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, deadEnd, Quaternion.Euler(0,90,0));
        }
         else if(result[0] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, deadEnd, Quaternion.Euler(0,180,0));
        }
    }
    private bool CreateStraightRoad(PlacementManager pm, CellType[] result, Vector3Int pos)
    {
        if(result[0] == CellType.Road && result[2] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, roadStraight, Quaternion.Euler(0,0,0));
            return true;
        } else if(result[1] == CellType.Road && result[3] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, roadStraight, Quaternion.Euler(0,90,0));
            return true;
        }
        return false;
    }
    private void CreateCorner(PlacementManager pm, CellType[] result, Vector3Int pos)
    {
        if(result[1] == CellType.Road && result[2] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, corner, Quaternion.Euler(0,90,0));
        } else if(result[2] == CellType.Road && result[3] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, corner, Quaternion.Euler(0,180,0));
        } else if(result[3] == CellType.Road && result[0] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, corner, Quaternion.Euler(0,270,0));
        }
         else if(result[0] == CellType.Road && result[1] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, corner, Quaternion.Euler(0,0,0));
        }
    }
    private void Create3Way(PlacementManager pm, CellType[] result, Vector3Int pos)
    {
        if(result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, threeWay, Quaternion.identity);
        } else if(result[2] == CellType.Road && result[3] == CellType.Road && result[0] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, threeWay, Quaternion.Euler(0,90,0));
        } else if(result[3] == CellType.Road && result[0] == CellType.Road && result[1] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, threeWay, Quaternion.Euler(0,180,0));
        }
         else if(result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road)
        {
            pm.ModifyStructureModel(pos, threeWay, Quaternion.Euler(0,270,0));
        }
    }
    private void Create4Way(PlacementManager pm, CellType[] result, Vector3Int pos)
    {
        pm.ModifyStructureModel(pos, fourWay, Quaternion.identity);
    }
}

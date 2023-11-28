using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDirector : MonoBehaviour
{
    private PlacementManager pm;
    public GameObject[] civPrefabs;

    public void SpawnAllAgents()
    {
        foreach (var house in pm.GetAllHouses())
        {
            SpawnAgent(house, pm.GetRandomSpecial());
        }
        foreach (var specialStruc in pm.GetAllSpecial())
        {
            SpawnAgent(house, pm.GetRandomHouse());
        }
    }
    private void SpawnAgent(StructureModel startStruct, StructureModel endStruct)
    {
        if(startStruct != null && endStruct != null)
        {
            var startPos = ((INeedingRoad)startStruct).RoadPos;
            var endPosPos = ((INeedingRoad)endStruct).RoadPos;
            var agent = Instantiate(GetRandomCiv(), startPos, Quaternion.identity);
            var path = pm.GetPathBetween(startPos, endPos, true);
            if(path.Count > 0)
            {
                path.Reverse();
                var aiAgent = agent.GetComponent<AIAgent>();
                aiAgent.Initialize(new List<Vector3>(path.Select(x =>(Vector3)x).ToList()));
            }
        }
    }
    private GameObject GetRandomCiv()
    {
        return civPrefabs[UnityEngine.Random.Range(0,civPrefabs.Length)];
    }
}

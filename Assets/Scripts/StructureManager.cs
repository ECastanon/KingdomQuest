﻿using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    private PlacementManager pm;
    public List<GameObject> housePrefabs;
    public List<GameObject> specialPrefabs;

    private void Start()
    {
        pm = GameObject.Find("PlacementManager").GetComponent<PlacementManager>();
    }
    public void PlaceHouse(Vector3Int position) //Passes house information to PlacementManager
    {
        if (CheckPositionBeforePlacement(position))
        {
            pm.PlaceOnMap(position, housePrefabs[0], CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    public void PlaceSpecial(Vector3Int position) //Passes special information to PlacementManager
    {
        if (CheckPositionBeforePlacement(position))
        {
            pm.PlaceOnMap(position, specialPrefabs[0], CellType.Structure);
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
        return true;
    }
}
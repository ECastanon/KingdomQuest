using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputManager im;
    private RoadManager rm;

    void Start()
    {
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        rm = GameObject.Find("RoadManager").GetComponent<RoadManager>();

        im.OnMouseClick += rm.PlaceRoad;
        im.OnMouseHold += rm.PlaceRoad;
        im.OnMouseUp += rm.FinishPlacingRoad;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraMovement cm;
    private InputManager im;
    private RoadManager rm;

    void Start()
    {
        cm = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        rm = GameObject.Find("RoadManager").GetComponent<RoadManager>();

        im.OnMouseClick += HandleMouseClick;
    }
    void Update()
    {
        cm.MoveCamera(new Vector3(im.camVect.x, 0, im.camVect.y));
        cm.CameraScroll();
        //cm.CameraBounds();
    }
    private void HandleMouseClick(Vector3Int position)
    {
        Debug.Log(position);
        rm.PlaceRoad(position);
    }
}

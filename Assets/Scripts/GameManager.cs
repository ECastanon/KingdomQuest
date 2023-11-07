using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cm;
    public InputManager im;
    public RoadManager rm;

    void Start()
    {
        im.OnMouseClick += HandleMouseClick;
    }
    void Update()
    {
        cm.MoveCamera(new Vector3(im.camVect.x, 0, im.camVect.y));
        cm.CameraScroll();
    }
    private void HandleMouseClick(Vector3Int position)
    {
        Debug.Log(position);
        rm.PlaceRoad(position);
    }
}

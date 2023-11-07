using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> OnMouseClick, OnMouseHold;
    public Action OnMouseUp;
    private Vector2 cameraVector;

    [SerializeField]
    Camera cam;

    public LayerMask groundMask;
    [HideInInspector] public Vector2 camVect;
    private void Update()
    {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
        CheckArrowInput();
    }
    private Vector3Int? RaycastGround()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }
        return null;
    }
    private void CheckClickDownEvent()
    {
        if(Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if(position != null)
            {
                OnMouseClick?.Invoke(position.Value);
            }
        }
    }
    private void CheckClickUpEvent()
    {
        if(Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            OnMouseUp?.Invoke();
        }
    }
    private void CheckClickHoldEvent()
    {
        if(Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if(position != null)
            {
                OnMouseHold?.Invoke(position.Value);
            }
        }
    }
    private void CheckArrowInput()
    {
        camVect = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
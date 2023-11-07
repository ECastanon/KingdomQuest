using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera gameCamera;
    public float cameraMovementSpeed = 5;

    private void Start()
    {
        gameCamera = GetComponent<Camera>();
    }
    public void MoveCamera(Vector3 inputVector)
    {
        var movementVector = Quaternion.Euler(0,30,0) * inputVector;
        gameCamera.transform.position += movementVector * Time.deltaTime * cameraMovementSpeed;
    }
    public void CameraScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && gameCamera.orthographicSize > 1.5f) //ZoomIn
        {
            gameCamera.orthographicSize -= .5f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && gameCamera.orthographicSize < 10f) //ZoomOut
        {
            gameCamera.orthographicSize += .5f;
        }
    }
}
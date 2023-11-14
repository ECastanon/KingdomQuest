using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera gameCamera;
    public float cameraMovementSpeed = 5;
    public int MinX, MaxX, MinZ, MaxZ;
    private Vector3 camVect;

    private void Start()
    {
        gameCamera = GetComponent<Camera>();
    }
    void Update()
    {
        camVect = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MoveCamera(new Vector3(camVect.x, 0, camVect.y));
        CameraScroll();
    }
    private void MoveCamera(Vector3 inputVector)
    {
        var movementVector = Quaternion.Euler(0, 30, 0) * inputVector;
        gameCamera.transform.position += movementVector * Time.deltaTime * cameraMovementSpeed;

        // Now compute the Clamp value
        float xPos = Mathf.Clamp(transform.position.x, MinX, MaxX);
        float zPos = Mathf.Clamp(transform.position.z, MinZ, MaxZ);

        // Update position.
        transform.position = new Vector3(xPos, transform.position.y, zPos);
    }
    private void CameraScroll()
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera gameCamera;
    public float cameraMovementSpeed = 5;
    public float MinimumXValue = -5;
    public float MaximumXValue = 5;
    public float MinimumZValue = -5;
    public float MaximumZValue = 5;
    public float pSpeed;

    private void Start()
    {
        gameCamera = GetComponent<Camera>();
    }
    public void MoveCamera(Vector3 inputVector)
    {
        var movementVector = Quaternion.Euler(0, 30, 0) * inputVector;
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
    public void CameraBounds()
    {
        if (Input.GetKey("w"))
        {
            if (transform.position.z > MinimumZValue && transform.position.z < MaximumZValue)
            {
                transform.Translate(Vector3.forward * pSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey("a"))
        {
            if (transform.position.x > MinimumXValue && transform.position.x < MaximumXValue)
            {
                transform.Translate(Vector3.left * pSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey("d"))
        {
            if (transform.position.x > MinimumXValue && transform.position.x < MaximumXValue)
            {
                transform.Translate(Vector3.right * pSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey("s"))
        {
            if (transform.position.z > MinimumZValue && transform.position.z < MaximumZValue)
            {
                transform.Translate(Vector3.back * pSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
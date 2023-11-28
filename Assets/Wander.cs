using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    private bool canRotate = true;
    private float currentRotY;

    private void OnTriggerEnter(Collider col)
    {
        if(canRotate == false)
        {
            return;
        }
        if(col.gameObject.transform.parent.name == "DeadEndRoad(Clone)")
        {
            Debug.Log(col.gameObject.transform.parent.name); //Reverses
            StartCoroutine("ChangeDirection", 180);
        } else
        if(col.gameObject.transform.parent.name == "CurvedRoad(Clone)")
        {
            Debug.Log(col.gameObject.transform.parent.name); //Find out if needs to turn left or right at at the curve collider
            StartCoroutine("ChangeDirection", col.gameObject.GetComponent<RotationToTurn>().rotationToTurn);
        } else
        if(col.gameObject.transform.parent.name == "3WayRoad(Clone)")
        {
            Debug.Log(col.gameObject.transform.parent.name);
            col.gameObject.GetComponent<RotationToTurn>(); //Decides if can turn left, right, or straight
            StartCoroutine("ChangeDirection", GetRandomDirectionThree(col));
        } else
        if(col.gameObject.transform.parent.name == "4WayRoad(Clone)")
        {
            Debug.Log(col.gameObject.transform.parent.name); //Randomly chooses left, right, straight
            StartCoroutine("ChangeDirection", GetRandomDirectionFour());
        }
    }
    IEnumerator ChangeDirection(float addRotation)
    { //Gets current rotation and adds or subtracts when needing to turn
        if(canRotate == true)
        {
            currentRotY = transform.localRotation.eulerAngles.y;
            Debug.Log("Changing Direction to: " + (currentRotY + addRotation));
            canRotate = false;
            yield return new WaitForSeconds(0.5f); //Waits to be out of the current tile colliders
            currentRotY = currentRotY + addRotation;
            transform.rotation = Quaternion.Euler(0, currentRotY, 0);
            yield return new WaitForSeconds(.8f); //Waits to be out of the current tile colliders
            Debug.Log("Ready for next turn");
            canRotate = true;
        }
    }
    private float GetRandomDirectionThree(Collider col)
    {
        int newRotation = 0;
        int rand = Random.Range(0, 1);
        if(col.gameObject.transform.name == "Collider1")
        {
            if(rand == 0){newRotation = -90;} //Left
            if(rand == 1){newRotation = 90;} //Right
        }
        if(col.gameObject.transform.name == "Collider2")
        {
            if(rand == 0){newRotation = 0;} //Straight
            if(rand == 1){newRotation = 90;} //Right
        }
        if(col.gameObject.transform.name == "Collider4")
        {
            if(rand == 0){newRotation = -90;} //Left
            if(rand == 1){newRotation = 0;} //Straight
        }
        return newRotation;
    }
    private float GetRandomDirectionFour()
    {
        int rand = Random.Range(0, 2);
        int newRotation = 0;
        switch (rand)
        {
        case 0: //Straight
            newRotation = 0;
            break;
        case 1: //Right
            newRotation = 90;
            break;
        case 2: //Left
            newRotation = -90;
            break;
        default:
            Debug.Log("Invalid TurnValue");
            break;
        }
        return newRotation;
    }
}

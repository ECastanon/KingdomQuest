using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    private bool canRotate = true;
    private float currentRotY;
    private bool wasInDeadEnd = false;

    void Update()
    {
        NotOnRoad();
    }
    private void OnTriggerEnter(Collider col)
    {
        if(canRotate == false)
        {
            return;
        }
        if(col.gameObject.transform.parent.name == "DeadEndRoad(Clone)")
        {
            Debug.Log(col.gameObject.transform.parent.name); //Reverses
            wasInDeadEnd = true;
            StartCoroutine("ChangeDirection", 180);
        } else
        if(col.gameObject.transform.parent.name == "CurvedRoad(Clone)")
        {
            Debug.Log(col.gameObject.transform.parent.name); //Find out if needs to turn left or right at at the curve collider
            wasInDeadEnd = false;
            StartCoroutine("ChangeDirection", col.gameObject.GetComponent<RotationToTurn>().rotationToTurn);
        } else
        if(col.gameObject.transform.parent.name == "3WayRoad(Clone)")
        {
            Debug.Log(col.gameObject.transform.parent.name);
            col.gameObject.GetComponent<RotationToTurn>(); //Decides if can turn left, right, or straight
            wasInDeadEnd = false;
            StartCoroutine("ChangeDirection", GetRandomDirectionThree(col));
        } else
        if(col.gameObject.transform.parent.name == "4WayRoad(Clone)")
        {
            Debug.Log(col.gameObject.transform.parent.name); //Randomly chooses left, right, straight
            wasInDeadEnd = false;
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
            yield return new WaitForSeconds(.9f); //Waits to be out of the current tile colliders
            Debug.Log("Ready for next turn");
            canRotate = true;
        }
    }
    IEnumerator Reverse()
    {
        currentRotY = transform.localRotation.eulerAngles.y + 180f;
        transform.rotation = Quaternion.Euler(0, currentRotY, 0);
        yield return new WaitForSeconds(1f);
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
    private void NotOnRoad()
    {
        int notGround = 0;
        Vector3 roadCheck = new Vector3(transform.position.x, 0.2f, transform.position.z);
        RaycastHit[] hit;
        hit = Physics.RaycastAll(roadCheck, Vector3.down, 1);
        Debug.DrawRay(roadCheck, Vector3.down, Color.yellow);
        for (int i = 0; i < hit.Length; i++)
        {
            if(hit[i].transform.gameObject.name != "Ground")
            {
                notGround++;
            }
        }
        if(notGround == 0) //If the only object underneath the civ is ground, civ warps to a valid road tile
        {
            if(wasInDeadEnd == true)
            {
                return;
                wasInDeadEnd = false;
            }
            Debug.Log("Out of Bounds! Fixing Location!");
            List<GameObject> straights = new List<GameObject>();
            foreach (var road in GameObject.Find("RoadManager").GetComponent<RoadManager>().roadList)
            {
                if(road.transform.name == "StraightRoad(Clone)" || road.transform.name == "DeadEndRoad(Clone)")
                {
                    straights.Add(road);
                }
            }
            int rand = Random.Range(0, straights.Count);
            transform.position = new Vector3(straights[rand].transform.position.x, 0.02f, straights[rand].transform.position.z);
            transform.rotation = Quaternion.Euler(0, straights[rand].transform.localRotation.eulerAngles.y, 0);
        }
    }
}

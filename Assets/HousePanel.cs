using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HousePanel : MonoBehaviour
{
    public int houseToUse = 0;
    private bool inView;
    public GameObject returnButton;

    void Start()
    {
        returnButton = transform.GetChild(0).gameObject;
        returnButton.SetActive(false);
    }
    public void SlideIn()
    {
        StartCoroutine(moveOverTime(transform, new Vector3(-335, -85, 0), 0.5f));
        returnButton.SetActive(true);
        inView = true;
    }
    public void SlideOut()
    {
        StartCoroutine(moveOverTime(transform, new Vector3(-500, -85, 0), 0.25f));
        returnButton.SetActive(false);
        inView = false;
        houseToUse = 0;
    }
    public void houseNumber(int num)
    {
        houseToUse = num;
    }
    IEnumerator moveOverTime(Transform pos, Vector3 newPos, float duration)
    {
        float counter = 0;

        //Get the current position of the object to be moved
        Vector3 newPosSize = pos.localPosition;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            pos.localPosition = Vector3.Lerp(newPosSize, newPos, counter / duration);
            yield return null;
        }
    }
}

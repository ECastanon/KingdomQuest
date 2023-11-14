using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement;
    private Button roadButton, houseButton, specialButton;

    public Color outlineColor;
    List<Button> buttonList;

    void Start()
    {
        roadButton = GameObject.Find("PlaceRoadBtn").GetComponent<Button>();
        houseButton = GameObject.Find("PlaceHouseBtn").GetComponent<Button>();
        specialButton = GameObject.Find("PlaceSpecialBtn").GetComponent<Button>();

        buttonList = new List<Button>(){roadButton, houseButton, specialButton};

        //Sets the button actions
        roadButton.onClick.AddListener(() =>
        {
            ResetColor();
            ModifyOutLine(roadButton);
            OnRoadPlacement?.Invoke();
        });
        houseButton.onClick.AddListener(() =>
        {
            ResetColor();
            ModifyOutLine(houseButton);
            OnHousePlacement?.Invoke();
        });
        specialButton.onClick.AddListener(() =>
        {
            ResetColor();
            ModifyOutLine(specialButton);
            OnSpecialPlacement?.Invoke();
        });
    }
    private void ModifyOutLine(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }
    private void ResetColor()
    {
        foreach(Button button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}

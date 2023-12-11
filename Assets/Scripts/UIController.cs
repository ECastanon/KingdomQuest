using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnMerchantPlacement, OnFarmPlacement, OnHospitalPlacement;
    private Button roadButton, houseButton, merchantButton, farmButton, hospitalButton;
    private GameObject warningPanel;
    public Color outlineColor;
    List<Button> buttonList;

    void Start()
    {
        roadButton = GameObject.Find("PlaceRoadBtn").GetComponent<Button>();
        houseButton = GameObject.Find("PlaceHouseBtn").GetComponent<Button>();
        merchantButton = GameObject.Find("PlaceMerchantBtn").GetComponent<Button>();
        farmButton = GameObject.Find("PlaceFarmBtn").GetComponent<Button>();
        hospitalButton = GameObject.Find("PlaceHospitalBtn").GetComponent<Button>();

        warningPanel = GameObject.Find("WarningPanel");
        warningPanel.SetActive(false);

        buttonList = new List<Button>() { roadButton, houseButton, merchantButton, farmButton, hospitalButton };

        //Sets the button actions
        roadButton.onClick.AddListener(() =>
        {
            ResetColor();
            ModifyOutLine(roadButton);
            OnRoadPlacement?.Invoke();
        });

        houseButton.onClick.AddListener(() =>
        {
            ResetColor(); //Checks if there are at least 2 valid road tiles to place houses
            List<GameObject> straights = new List<GameObject>();
            foreach (var road in GameObject.Find("RoadManager").GetComponent<RoadManager>().roadList)
            {
                if (road.transform.name == "StraightRoad(Clone)" || road.transform.name == "DeadEndRoad(Clone)")
                {
                    straights.Add(road);
                }
            }
            if (straights.Count >= 2)
            {
                warningPanel.SetActive(false);
                ModifyOutLine(houseButton);
                OnHousePlacement?.Invoke();
            }
            else
            {
                ShowWarning("You must have at least 2 roads before building a house!");
            }

        });

        merchantButton.onClick.AddListener(() =>
        {
            ResetColor();
            ModifyOutLine(merchantButton);
            OnMerchantPlacement?.Invoke();
        });
        farmButton.onClick.AddListener(() =>
        {
            ResetColor();
            ModifyOutLine(farmButton);
            OnFarmPlacement?.Invoke();
        });
        hospitalButton.onClick.AddListener(() =>
        {
            ResetColor();
            ModifyOutLine(hospitalButton);
            OnHospitalPlacement?.Invoke();
        });
    }
    public void ShowWarning(string stringToPass)
    {
        warningPanel.SetActive(true);
        if (warningPanel.activeSelf)
        {
            // Check if the warningPanel is active before accessing its components
            FadeOut fadeOut = warningPanel.GetComponent<FadeOut>();
            if (fadeOut != null)
            {
                fadeOut.OnReveal(stringToPass);
            }
        }
    }

    private void ModifyOutLine(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetColor()
    {
        foreach (Button button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}

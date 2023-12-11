using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputManager im;
    private RoadManager rm;
    private StructureManager sm;
    private UIController uc;

    void Start()
    {
        im = GameObject.Find("InputManager").GetComponent<InputManager>();
        rm = GameObject.Find("RoadManager").GetComponent<RoadManager>();
        sm = GameObject.Find("StructureManager").GetComponent<StructureManager>();
        uc = GameObject.Find("Canvas").GetComponent<UIController>();

        //Selects the type to place after selecting a button
        uc.OnRoadPlacement += RoadPlacementHandler; //Clicks will place roads
        uc.OnHousePlacement += HousePlacementHandler; //Clicks will place houses
        uc.OnMerchantPlacement += MerchantPlacementHandler;
        uc.OnFarmPlacement += FarmPlacementHandler;
        uc.OnHospitalPlacement += HospitalPlacementHandler;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();

        im.OnMouseClick += rm.PlaceRoad;
        im.OnMouseHold += rm.PlaceRoad;
        im.OnMouseUp += rm.FinishPlacingRoad;
    }
    private void HousePlacementHandler()
    {
        ClearInputActions();

        im.OnMouseClick += sm.PlaceHouse;
        im.OnMouseHold += sm.PlaceHouse;
    }
    private void SpecialPlacementHandler()
    {
        ClearInputActions();

        im.OnMouseClick += sm.PlaceSpecial;
        im.OnMouseHold += sm.PlaceSpecial;
    }
    private void MerchantPlacementHandler()
    {
        ClearInputActions();

        im.OnMouseClick += sm.PlaceMerchant;
        im.OnMouseHold += sm.PlaceMerchant;
    }
    private void FarmPlacementHandler()
    {
        ClearInputActions();

        im.OnMouseClick += sm.PlaceFarm;
        im.OnMouseHold += sm.PlaceFarm;
    }
    private void HospitalPlacementHandler()
    {
        ClearInputActions();

        im.OnMouseClick += sm.PlaceHospital;
        im.OnMouseHold += sm.PlaceHospital;
    }
    private void ClearInputActions()
    {
        im.OnMouseClick = null;
        im.OnMouseHold = null;
        im.OnMouseUp = null;
    }
    
}

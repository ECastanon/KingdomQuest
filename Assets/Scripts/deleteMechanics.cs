using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class deleteMechanics : MonoBehaviour
{
    public ResourceManager resourceManager; 
    private string[] defaultModelsToDelete = { "road", "house", "merchant", "special" };
    public string[] modelsToDelete;
    private int i = 0;
    

    void Start()
    {
        if (modelsToDelete == null || modelsToDelete.Length == 0)
        {
            modelsToDelete = defaultModelsToDelete;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check the hit object and its parents for the specified models
                Transform currentTransform = hit.transform;
                while (currentTransform != null)
                {
                    if (Array.Exists(modelsToDelete, model => currentTransform.tag.Equals(model)))
                    {
                        Debug.Log("Deleting model with tag: " + currentTransform.tag);
                        if (currentTransform.tag == "road")
                        {
                            resourceManager.goldCount += resourceManager.roadCost;
                        }
                        if (currentTransform.tag == "house")
                        {
                            i++;
                            resourceManager.goldCount += resourceManager.houseCost;
                            if (i == 2 ) {
                              resourceManager.goldCount -= resourceManager.houseCost;
                              i = 0;
                            }
                        }
                        if (currentTransform.tag == "special")
                        {
                            i++;
                            resourceManager.goldCount += resourceManager.houseCost;
                            if (i == 2 ) {
                              resourceManager.goldCount -= resourceManager.houseCost;
                              i = 0;
                            }
                            
                        }
                        if (currentTransform.tag == "merchant")
                        {
                            resourceManager.goldCount += resourceManager.merchantCost;
                        }
                        if (currentTransform.tag == "farm")
                        {
                            resourceManager.goldCount += resourceManager.merchantCost;
                        }
                        GameObject.Find("PlacementManager").GetComponent<PlacementManager>().RemoveFromDictionary(Vector3Int.FloorToInt(currentTransform.position));
                        Destroy(currentTransform.gameObject);
                    }
                    // Move to the parent transform
                    Debug.Log("moving to parent");
                    //resourceManager.goldCount -= resourceManager.houseCost;
                    currentTransform = currentTransform.parent;
                    
                }
            }
        }
    }
}



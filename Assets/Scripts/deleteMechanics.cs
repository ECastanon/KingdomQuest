using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class deleteMechanics : MonoBehaviour
{
    public ResourceManager resourceManager; 
    // Define the default models for the objects you want to delete
    private string[] defaultModelsToDelete = { "road", "house", "merchant", "special" };

    // Use this array in case you don't set custom models in the Inspector
    public string[] modelsToDelete;

    void Start()
    {
        // If no custom models are set in the Inspector, use the default models
        if (modelsToDelete == null || modelsToDelete.Length == 0)
        {
            modelsToDelete = defaultModelsToDelete;
        }
    }

    void Update()
    {
        // Check if the left mouse button is pressed and the Ctrl key is held down
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any objects
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has one of the specified models
                if (hit.collider != null && Array.Exists(modelsToDelete, model => hit.collider.gameObject.tag.Equals(model)))
                {
                    Debug.Log("Deleting model with tag: " + hit.collider.gameObject.tag);
                    if (hit.collider.gameObject.tag == "road") {
                        resourceManager.goldCount += resourceManager.roadCost;
                    }
                    if (hit.collider.gameObject.tag == "house") {
                        resourceManager.goldCount += resourceManager.houseCost;
                    }
                    if (hit.collider.gameObject.tag == "special") {
                        resourceManager.goldCount += resourceManager.houseCost;
                    }
                    if (hit.collider.gameObject.tag == "merchant") {
                        resourceManager.goldCount += resourceManager.merchantCost;
                    }
                    // Delete the object
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePointer : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance;
    public GameObject interactableObject;
    public LayerMask ignoreLayers;


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxDistance, ~ignoreLayers))
        {
            if (hit.transform.tag.Equals(interactableObject.name))
            {

            }
            else
            {
            }

        }
        else
        {
        }
    }
}

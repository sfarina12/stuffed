using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    public Camera playerCamera;
    [Space, Header("Settings")]
    [Tooltip("max distance witch player can interact with objects")]
    public float maxDistance;
    public KeyCode interactionKey;
    public GameObject interactableText;
    public bool invertActiveText=false;
    public LayerMask ignoreLayers;
    public bool isInteractingAtStart = false;

    [HideInInspector]
    public bool isinteracting = false;
    bool hasClicked = false;

    private void Start()
    {
        if (isInteractingAtStart)
        { 
            isinteracting = true;
            hasClicked = !hasClicked;
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxDistance, ~ignoreLayers))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
            
            if (hit.transform.tag.Equals("interactable"))
            {
                if (invertActiveText)
                {
                    if (!hasClicked)
                        interactableText.active = true;
                    else
                        interactableText.active = false;
                }
                else
                    interactableText.active = true;

                if (Input.GetKeyDown(interactionKey)) { isinteracting = true; hasClicked = !hasClicked; }
                else { isinteracting = false; }
            }
            else
            {
                interactableText.active = false;
                isinteracting = false;
            }

        }
        else
        { 
            interactableText.active = false;
            isinteracting = false;
        }
    }
}

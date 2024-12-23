using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endSecondScene : MonoBehaviour
{
    public float timeForActivate = 1;
    public GameObject ui;

    float actTime = 0;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("interactable"))
            if((actTime >= timeForActivate) && !ui.activeSelf)
                ui.SetActive(true);

        actTime += Time.deltaTime;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkScrollValue : MonoBehaviour
{
    public Scrollbar bar;
    public GameObject up;
    public GameObject down;

    bool oneTime = true;
    void Update()
    {
        if (bar.value < 0.1 || bar.value > 0.9)
            switchEnables();
        else
            oneTime = false;
    }

    void switchEnables() {
        if (!oneTime)
        {
            up.SetActive(!up.activeSelf);
            down.SetActive(!down.activeSelf);
        }
        oneTime = true;
    }
}

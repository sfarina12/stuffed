using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timeSystem : MonoBehaviour
{
    public TextMeshProUGUI time;
    public float minutesDuration = 1;

    float actTime = 0;
    int m = 45;
    int h = 5;

    void Update()
    {
        if (actTime >= minutesDuration)
        {
            m++;
            if (m >= 60) { h += 1; m = 0; }

            time.text = h + ":" + m + " PM";

            actTime = 0;
        }

        actTime+=Time.deltaTime;
    }
}

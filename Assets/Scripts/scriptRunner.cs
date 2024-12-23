using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptRunner : MonoBehaviour
{
    public startScene_1 ss_1;
    [HideInInspector]
    public bool isRunning = false;

    public void runScript()
    {
        if (ss_1 != null)
            ss_1.playAnimation();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracktorJumpScare : MonoBehaviour
{
    [Tooltip("in seconds")]
    public float jumpscareTime = 5;
    [Tooltip("to determinate if it's looking left or right")]
    public float maxCamRotation=45;
    [Space]
    public GameObject tracktorL;
    public GameObject tracktorR;
    public GameObject camera;

    bool startTime = false;
    float actTime = 0;
    bool isLookingLeft = false;
    bool oneTime=false;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("roads"))
        {
            actTime = 0;
            startTime = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("roads"))
        {
            startTime = false;
            actTime = 0;
        }
    }

    private void Update()
    {
        if ((actTime >= jumpscareTime) && !oneTime)
        {
            oneTime = true;
            if (isLookingLeft)
            {
                tracktorR.SetActive(true);
                tracktorL.SetActive(false);
            }
            else
            {
                tracktorR.SetActive(false);
                tracktorL.SetActive(true);
            }
        }

        if (startTime)
            actTime += Time.deltaTime;

        float rotation = camera.transform.localRotation.eulerAngles.y;

        if (rotation > 0 && rotation < maxCamRotation)
            isLookingLeft = false;

        if (rotation < 360 && rotation > (360 - maxCamRotation))
            isLookingLeft = true;
    }
}

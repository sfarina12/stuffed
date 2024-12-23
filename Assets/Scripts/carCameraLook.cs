using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carCameraLook : MonoBehaviour
{
    public GameObject cameraOBJ;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public bool invertY = false;
    public bool enableReset = true;
    public float resetTime = 1;

    Quaternion originaPos;
    float rotationX = 0;
    float rotationY = 0;
    float act_time = 0;
    bool canReset = false;
    [HideInInspector]
    public bool canLook = true;

    void Start()
    {
        originaPos = cameraOBJ.transform.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (canLook)
        {
            rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            cameraOBJ.transform.localRotation = Quaternion.Euler((invertY ? -1 : 1) * rotationX, 0, 0);
            rotationY = Input.GetAxis("Mouse X") * lookSpeed;
            transform.localRotation *= Quaternion.Euler(0, rotationY, 0);

            if (enableReset)
            {
                if (rotationY == 0)
                {
                    if (act_time > resetTime)
                    {
                        canReset = true;
                        act_time = 0;
                        rotationY = 0;
                    }
                    else
                        act_time += Time.deltaTime;
                }
                else
                {
                    act_time = 0;
                    canReset = false;
                }

                if (canReset)
                    resetPos();
            }
        }
    }

    public void showPointer(bool isVisible,bool isLocked)
    {
        Cursor.visible = isVisible;

        if(isLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
    private void resetPos()
    {
        rotationX = Mathf.Lerp(rotationX,0,Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carCameraDump : MonoBehaviour
{
    public GameObject cameraOBJ;
    public Rigidbody carRB;
    public float maxDamp=4;
    public float fovMultiplier=2;

    float orifinalFov;

    void Start()
    {
        orifinalFov = cameraOBJ.GetComponent<Camera>().fieldOfView;
    }

    void Update()
    {
        float x=0, y=0, z=0;
        float fov = orifinalFov;

        if (Input.GetKey(KeyCode.W))
            z = maxDamp;
        
        if (Input.GetKey(KeyCode.A))
            x = maxDamp;

        if (Input.GetKey(KeyCode.S))
            z = -maxDamp;

        if (Input.GetKey(KeyCode.D))
            x = -maxDamp;

        Vector3 pos = new Vector3(x,y,z);

        fov = fov + (fovMultiplier * ((z<0?-1:1)*z)) + (fovMultiplier * ((x < 0 ? -1 : 1) * x));

        if(carRB.velocity.magnitude<1)
            pos = pos* carRB.velocity.magnitude;

        cameraOBJ.transform.localPosition = Vector3.Lerp(cameraOBJ.transform.localPosition, pos, Time.deltaTime);
        cameraOBJ.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cameraOBJ.GetComponent<Camera>().fieldOfView, fov,Time.deltaTime);

        //cameraOBJ.transform.localPosition = Vector3.Lerp(cameraOBJ.transform.localPosition, carRB.velocity.normalized, Time.deltaTime);
    }
}

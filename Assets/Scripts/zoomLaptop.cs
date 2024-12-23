using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomLaptop : MonoBehaviour
{
    public Transform laptopPosition;
    public Transform camera;
    public carCameraLook look;
    public interact interacter;
    public float speed = 1;
    public float minDistance = 0.5f;
    [Space]
    public carCameraDump dump;
    public carController controller;
    public bool overrideOriginalPos=false;
    bool canZoom = true;
    bool isZoomedIn=false;
    Vector3 originalPos;
    Quaternion originalRot;

    private void Start()
    {
        if (!overrideOriginalPos)
            originalPos = camera.position;
        else
            originalPos = Vector3.zero;
    }

    private void Update()
    {
        if (interacter.isinteracting && canZoom)
        {
            isZoomedIn = !isZoomedIn;

            if (isZoomedIn)
                StartCoroutine(zoomIn());
            else
                StartCoroutine(zoomOut());
        }
    }

    IEnumerator zoomIn()
    {
        if (dump != null) dump.enabled = false;
        if (controller != null) controller.enabled = false;

        originalRot = camera.rotation;
        canZoom = false;
        look.canLook = false;
       

        while (Vector3.Distance(camera.position, laptopPosition.position) > minDistance)
        {
            camera.position = Vector3.Lerp(camera.position, laptopPosition.position, Time.deltaTime*speed);
            camera.rotation = Quaternion.Lerp(camera.rotation, laptopPosition.rotation, Time.deltaTime * speed);
            yield return null;
        }

        look.showPointer(true, false);
        canZoom = true;
    }

    IEnumerator zoomOut()
    {        
        if(!overrideOriginalPos)
            while (Vector3.Distance(camera.position, originalPos) > minDistance)
            {
                if(!overrideOriginalPos)
                    camera.position = Vector3.Lerp(camera.position, originalPos, Time.deltaTime * speed);
                else
                    camera.localPosition = Vector3.Lerp(camera.localPosition, originalPos, Time.deltaTime * speed);

                camera.rotation = Quaternion.Lerp(camera.rotation, originalRot, Time.deltaTime * speed);
                yield return null;
            }

        look.showPointer(false, true);
        look.canLook = true;
        canZoom = true;

        if (dump != null) dump.enabled = true;
        if (controller != null) controller.enabled = true;
    }

}

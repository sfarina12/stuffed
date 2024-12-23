using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSnavigator : MonoBehaviour
{
    public Vector2 minCoordinates_GPS;
    public Vector2 maxCoordinates_GPS;
    [Space]
    public RectTransform GPS_map;
    public Transform youIndicator;
    public GameObject Car;
    private void Update()
    {
        Vector3 tmp = calculateCoordinates();
        GPS_map.localPosition = new Vector3(tmp.x,tmp.z,0);
        youIndicator.localRotation = Quaternion.Euler(0,0,Car.transform.rotation.eulerAngles.y);
    }

    private Vector3 calculateCoordinates()
    {
        Vector3 carP = Car.transform.position;

        carP.x = ((maxCoordinates_GPS.x - minCoordinates_GPS.x) * ((carP.x - 155f) / (701f))) + minCoordinates_GPS.x;
        carP.z = ((maxCoordinates_GPS.y - minCoordinates_GPS.y) * ((carP.z - 156f) / (672f))) + minCoordinates_GPS.y;

        return carP;
    }
}

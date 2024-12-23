using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateStreets : MonoBehaviour
{
    public GameObject start;
    public Texture2D streetGrid;
    public Terrain terrain;
    public float multiplier = 1;

    public GameObject street_streight;
    public GameObject curve;
    public GameObject intersection_x;
    public GameObject intersection_t;

    const string STR_Y = "7F7F7FFF";
    const string STR_X = "000000FF";

    const string CUR_1 = "BF0000FF";
    const string CUR_2 = "7F0000FF";
    const string CUR_3 = "3F0000FF";
    const string CUR_4 = "FF0000FF";

    const string INT_A = "4CFF00FF";

    const string INT_B_1 = "001CBFFF";
    const string INT_B_2 = "00137FFF";
    const string INT_B_3 = "00093FFF";
    const string INT_B_4 = "0026FFFF";

    void Start()
    {
        for (int x = 0; x < streetGrid.width; x++)
        {
            for (int y = 0; y < streetGrid.height; y++)
            {
                if (!streetGrid.GetPixel(x, y).Equals(Color.white))
                {
                    string color = ColorUtility.ToHtmlStringRGBA(streetGrid.GetPixel(x, y));

                    switch (color)
                    {
                        case STR_X: placeRoad(street_streight, x, y, 90, false); break;
                        case STR_Y: placeRoad(street_streight, x, y, 0, false); break;

                        case CUR_1: placeRoad(curve, x, y, -90,true); break;
                        case CUR_2: placeRoad(curve, x, y, 0, true); break;
                        case CUR_3: placeRoad(curve, x, y, 90, true); break;
                        case CUR_4: placeRoad(curve, x, y, 180, true); break;

                        case INT_A: placeRoad(intersection_x, x, y, 0, true); break;

                        case INT_B_1: placeRoad(intersection_t, x, y, 0, true); break;
                        case INT_B_2: placeRoad(intersection_t, x, y, 90, true); break;
                        case INT_B_3: placeRoad(intersection_t, x, y, 180, true); break;
                        case INT_B_4: placeRoad(intersection_t, x, y, 270, true); break;
                    }

                }
            }
        }
    }

    private void placeRoad(GameObject obj,int x, int y, int rot,bool isCurve)
    {
        GameObject street = Instantiate(obj, Vector3.zero,Quaternion.Euler(0,0,0));
        street.transform.parent = start.transform;

        Mesh mesh;

        if(!isCurve)
            mesh = street.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
        else
            mesh = street_streight.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;

        Vector3 max = mesh.bounds.max;

        Vector3 pos = new Vector3(x* max.x*2, 0, y*max.y);      

        street.transform.localPosition = pos;
        street.transform.rotation = Quaternion.Euler(0, rot, 0);
    }

    private float calculateTerrainHeight(float x, float y)
    {
        int X = (int)start.transform.position.x + (int)x;
        int Z = (int)start.transform.position.z + (int)y;

        float Y = terrain.terrainData.GetHeight(X,Z);

        return Y*multiplier;
    }
}

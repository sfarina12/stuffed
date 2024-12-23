using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gpsNavigator : MonoBehaviour
{
    public List<GameObject> gpsPoints;
    public GameObject player;
    public GameObject destination;

    List<float[]> distances;
    private void Start()
    {
        distances = new List<float[]>();

        foreach (GameObject point in gpsPoints)
        {
            string[] names = point.name.Split("-");
            
            float[] temp = new float[names.Length];

            for(int i=1,u=0;i<names.Length;i++,u++)
            {
                Vector3 obj = findPoint(names[i]);
                temp[u]=(Vector3.Distance(point.transform.position, obj));
            }
            distances.Add(temp);
        }

        List<nodo> ns = Dijkstra();

        foreach (nodo n in ns)
        {
            Debug.Log(n.obj.name+" - "+n.distanza);
        }
    }

    public List<nodo> Dijkstra()
    {
        List<nodo> Q = new List<nodo>();
        List<nodo> T = new List<nodo>();

        GameObject start = gpsPoints[0];
        Q.Add(new nodo(0, start));

        foreach (GameObject point in gpsPoints)
        {
            if (!point.Equals(start))
                Q.Add(new nodo(99, point));
        }
        
        T.Add(Q[0]);
        Q.RemoveAt(0);

        while (Q.Count > 0)
        {
            nodo v = extractMin(Q);
            
            T.Add(v);
            
            string[] names = v.obj.name.Split("-");
            
            for (int i = 1, u = 0; i < names.Length; i++, u++)
            {
                Debug.Log(getInQ(getObjByNode(names[i]), Q));
                float dis = distanzaTraVeZ(getInQ(getObjByNode(names[i]), Q), getInQ(getObjByNode(names[i]), Q));
            

                if (isInT(getObjByNode(names[i]), Q) && (v.distanza + dis) > (getInQ(getObjByNode(names[i]), Q).distanza))
                {
                    nodo n = v;

                    foreach (nodo nn in Q)
                    {
                        if (nn.obj.name.Equals(n.obj.name))
                        { 
                            nn.distanza = dis;
                            Debug.Log("4");
                        }
                        
                    }

                }
            }
        }

        return T;
    }

    private float distanzaTraVeZ(nodo v, nodo z)
    {
        for (int i=0;i<gpsPoints.Count;i++)
        {
            if (gpsPoints[i].name.Equals(z.obj.name))
            {
                string[] names = v.obj.name.Split("-");

                for (int j = 1; j < names.Length; j++)
                {
                    if (v.obj.name.Contains(names[j]))
                    {
                        return distances[i][j];
                    }
                }
            }
        }
        return 99;
    }

    private GameObject getObjByNode(string name)
    {
        foreach (GameObject point in gpsPoints)
        {
            if (point.name.StartsWith(name))
                return point;
        }

        return null;
    }

    private bool isInT(GameObject obj, List<nodo> T)
    {
        foreach (nodo point in T)
        {
            if (point.obj.name.Equals(obj.name))
                return true;
        }

        return false;
    }

    private nodo getInQ(GameObject obj, List<nodo>Q)
    {
        foreach (nodo point in Q)
        {
            if (point.obj.name.Equals(obj.name))
            {
                return point; 
            }
        }

        return null;
    }

    private nodo extractMin(List<nodo> Q)
    {
        nodo min = new nodo(Q[0].distanza, Q[0].obj);
        int index = 0;
        int i = 0;
        foreach (nodo point in Q)
        {
            if (point.distanza <= min.distanza)
            {
                min.distanza = point.distanza;
                min.obj = point.obj;
                
                index = i;
            }
            i++;
        }

        Q.RemoveAt(index);
        
        return min;
    }

    public  class nodo {
        public float distanza;
        public GameObject obj;

        public nodo(float distanza, GameObject obj)
        {
            this.distanza = distanza;
            this.obj = obj;
        }

        public override string ToString()
        {
            return distanza + " " + obj.name;
        }

    }

    private Vector3 findPoint(string v)
    {
        foreach (GameObject point in gpsPoints)
        {
            if (point.name.StartsWith(v))
                return point.transform.position;
        }

        return Vector3.positiveInfinity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ADVnodeReader_NoCooroutine : MonoBehaviour
{
    public SceneUno_dialogueGraph graph;
    public bool startWhenEnabled = true;
    [Space, Header("Additional scripts")]
    public chatController controller;
    [Space, Header("Graphics")]
    public GameObject youPrefab;
    public GameObject hePrefab;

    Coroutine _parser;
    private void Start()
    {
        if(graph!=null)
            if (startWhenEnabled)
            {
                foreach (BaseNode b in graph.nodes)
                    if (b.GetString().Equals("Start")) { graph.current = b; break; }

                readNode();
            }
    }

    //utilizzato all'interno dell'update
    public void beginDialogue()
    {
        foreach (BaseNode b in graph.nodes)
            if (b.GetString().Equals("Start")) { graph.current = b; break; }

        readNode();
    }

    public void readNode() {

        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');

        if (dataParts[0].Equals("Start")) { nextNode("exit"); }
        if (dataParts[0].Equals("ADV_sentence"))
        {
            controller.buildGraphics(dataParts[1], false);
        }
        if (dataParts[0].Equals("ADV_Endplus")) { controller.buildGraphics(dataParts[1], false); controller.disableChat(); return; }
        if (dataParts[0].Equals("End")) { controller.disableChat(); return; }
    }

    public void nextNode(string fieldName)
    {
        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName.Equals(fieldName))
            { graph.current = p.Connection.node as BaseNode; break; }
        }

        readNode();
    }
}

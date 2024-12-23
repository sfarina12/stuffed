using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XNode;

public class AdvancedNodeReader : MonoBehaviour
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
        if (startWhenEnabled)
        {
            foreach (BaseNode b in graph.nodes)
                if (b.GetString().Equals("Start")) { graph.current = b; break; }

            _parser = StartCoroutine(ParserNode());
        }
    }

    //utilizzato all'interno dell'update
    public void beginDialogue()
    {
        foreach (BaseNode b in graph.nodes)
            if (b.GetString().Equals("Start")) { graph.current = b; break; }

        _parser = StartCoroutine(ParserNode());
    }
    
    IEnumerator ParserNode()
    {
        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');

        if (dataParts[0].Equals("Start")) { nextNode("exit"); }
        if (dataParts[0].Equals("ADV_sentence"))
        {
            controller.buildGraphics(dataParts[1],false);

            /*while (!controller.canContinue)
                yield return null;

            controller.canContinue = false;
            
            nextNode(controller.isYes?"yes":"no");*/
        }
        if (dataParts[0].Equals("ADV_Endplus")) { controller.buildGraphics(dataParts[1], false); controller.disableChat(); yield break; }
        if (dataParts[0].Equals("End")) { controller.disableChat(); yield break; }
    }

    public void nextNode(string fieldName)
    {
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }
        
        foreach (NodePort p in graph.current.Ports)
        {
            if (p.fieldName.Equals(fieldName))
            { graph.current = p.Connection.node as BaseNode; break; }
        }

        _parser = StartCoroutine(ParserNode());
    }
}

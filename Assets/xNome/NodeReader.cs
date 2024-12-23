using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XNode;

public class NodeReader : MonoBehaviour
{
    public SceneUno_dialogueGraph graph;
    public bool startWhenEnabled = true;
    [Space,Header("Graphics")]
    public TextMeshProUGUI dialogueBody_UI;
    public GameObject dialogueInterface;
    public Animator anim;
    [Space,Header("Additional scripts")]
    public typewriterUI typeWriterEffect;
    public scriptRunner runner;
    [Space]
    public startGame levelLoader;
    
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

        if (dataParts[0].Equals("Start"))
        { 
            dialogueInterface.SetActive(true);
            nextNode("exit");
        }
        if (dataParts[0].Equals("SentenceNode"))
        {
            //dialogueBody_UI.text = dataParts[1];
            typeWriterEffect.textToWrite = dataParts[1];
            typeWriterEffect.restart = true;

            if (anim != null)
            {
                anim.SetBool("isTalkingAnimated", dataParts[2].Equals("True") ? true : false);
                animatorConditionsChecker(dataParts, false);
            }
            
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            nextNode("exit");
        }
        if (dataParts[0].Equals("End"))
        {
            dialogueInterface.SetActive(false);
            if(anim!=null)
                if(dataParts[2].Equals("True"))
                    animatorStopAll();

            if (levelLoader != null)
                levelLoader.loadLevel();

            yield break;
        }
        if (dataParts[0].Equals("EndPlus"))
        {
            if (dataParts[1].Equals("True"))
                runner.runScript();
            else
                runner.runScript();

            dialogueInterface.SetActive(false);

            if (levelLoader != null)
                levelLoader.loadLevel();

            yield break;
        }
    }

    private void animatorConditionsChecker(string[] dataParts,bool isEnding)
    {
        if(!isEnding)
            for (int i = 3; i < dataParts.Length; i++)
                anim.SetBool(dataParts[i], !anim.GetBool(dataParts[i])); 
        else
            for (int i = 1; i < dataParts.Length; i++)
                anim.SetBool(dataParts[i], !anim.GetBool(dataParts[i]));    
    }

    private void animatorStopAll()
    {
        int parameterCount = anim.parameterCount;

        for (int i = 2; i < parameterCount; i++)
        {
            string parameterName = anim.GetParameter(i).name;
            anim.SetBool(parameterName, false);
        }
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
            if(p.fieldName.Equals(fieldName))
            { graph.current = p.Connection.node as BaseNode; break; }
        }

        _parser = StartCoroutine(ParserNode());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSchatter : MonoBehaviour
{
    public interact interacter;
    public Animator anim;

    bool a = false;
    private void Start()
    {
        if (interacter.isInteractingAtStart)
        { 
            anim.Play("openGPSchat");
            a = !a;
        }
    }

    
    private void Update()
    {
        if (interacter.isinteracting)
        { anim.Play(!a ? "openGPSchat" : "closeGPSchat"); a = !a; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steerVolante : MonoBehaviour
{
    public Animator animator;
    public string sinistra;
    public string destra;
    public string still;

    bool SX=false;
    bool DX=false;
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!SX)
            { 
                //animator.Play(destra); SX = true; DX = false;
                animator.SetBool("SX",true);
                animator.SetBool("DX",false);
                animator.SetBool("STL", false);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!DX)
            { 
                //animator.Play(sinistra); DX = true; SX = false;
                animator.SetBool("SX", false);
                animator.SetBool("DX", true);
                animator.SetBool("STL", false);
            }
        }
        else
        {
            //animator.Play(still);
            DX = false; SX = false;
            animator.SetBool("SX", false);
            animator.SetBool("DX", false);
            animator.SetBool("STL", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScene_1 : MonoBehaviour
{
    public Animator anim;
    public string animationName = "start_2";

    public void playAnimation()
    {
        anim.Play(animationName);
    }
}

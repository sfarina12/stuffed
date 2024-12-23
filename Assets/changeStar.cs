using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeStar : MonoBehaviour
{
    Animator anim;
    public bool isOn = false;
    private void Start() { anim = gameObject.GetComponent<Animator>(); }
    public void changeStarState() { anim.Play(isOn?"starOff":"star"); isOn = !isOn; }
}

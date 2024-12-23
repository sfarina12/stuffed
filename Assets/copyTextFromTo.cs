using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class copyTextFromTo : MonoBehaviour
{
    public TextMeshProUGUI from;
    public TextMeshProUGUI to;
    private void Update()
    {
        to.text = from.text;
    }
}

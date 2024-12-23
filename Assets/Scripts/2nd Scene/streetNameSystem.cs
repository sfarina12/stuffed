using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class streetNameSystem : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnTriggerEnter(Collider other)
    {
        text.text = other.name;
    }
}

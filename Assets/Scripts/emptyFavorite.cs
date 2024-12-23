using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyFavorite : MonoBehaviour
{
    public GameObject defaultText;

    void Update()
    {
        if ((transform.childCount == 3) && !transform.GetChild(2).gameObject.activeSelf)
            defaultText.SetActive(true);
        else
            defaultText.SetActive(false);
    }
}

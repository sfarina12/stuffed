using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navbar : MonoBehaviour
{
    public List<app> apps;
    public string lastOpened;
    
    void Start()
    {
        effectOpen(null);
    }

    public void openApp(GameObject appIcon)
    {
        foreach (app a in apps)
            if (a.icon.name.Equals(appIcon.name))
            {
                if (!lastOpened.Equals(appIcon.name))
                { 
                    effectOpen(a);
                    lastOpened = appIcon.name;
                }
                else
                {
                    closeApp(appIcon);
                    lastOpened = "";
                }
            }
    }

    public void closeApp(GameObject appIcon)
    {
        foreach (app a in apps)
            if (a.icon.name.Equals(appIcon.name))
                a.content.SetActive(false);
    }

    private void effectOpen(app b)
    {
        foreach (app a in apps)
            a.content.SetActive(false);
        
        if(b!=null)
            b.content.SetActive(true);
    }

    [System.Serializable]
    public class app {
        public GameObject icon;
        public GameObject content;
    }
}

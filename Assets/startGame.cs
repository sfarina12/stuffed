using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public Animator anim;
    public int levelToLoad = 0;

    public void clickedStart()
    {
        anim.Play("clickStart");
    }

    public void loadLevel()
    {
        StartCoroutine(loadSceneCoroutine());
    }

    IEnumerator loadSceneCoroutine()
    {
        SceneManager.LoadScene(levelToLoad);
        yield return 0;
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(levelToLoad));
    }
}

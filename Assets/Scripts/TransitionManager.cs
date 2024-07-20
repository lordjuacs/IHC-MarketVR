using Meta.WitAi.Lib;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        string dishName = sceneName.Split('_')[1];
        sceneName = sceneName.Split('_')[0];
        AutoSingleton.dishName = dishName;
        StartCoroutine(LoadSceneAfterDelay(sceneName, 3f)); // 3 seconds delay
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    
}

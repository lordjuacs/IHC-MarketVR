using Meta.WitAi.Lib;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewTransitionManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        AutoSingleton.itemsText = "Cart:\n\n";
        AutoSingleton.items.Clear();
        StartCoroutine(LoadSceneAfterDelay(sceneName, 3f)); // 3 seconds delay
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }


}

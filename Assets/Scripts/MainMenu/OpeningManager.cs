using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return YieldCache.WaitForSeconds(0.5f);

        gameObject.GetComponent<Animator>().SetTrigger("Start");

        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");

        operation.allowSceneActivation = false;

        yield return YieldCache.WaitForSeconds(1.25f);

        operation.allowSceneActivation = true;
    }
}

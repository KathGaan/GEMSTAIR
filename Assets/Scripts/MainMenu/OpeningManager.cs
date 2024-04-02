using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] SoundClip clip = new SoundClip();

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return YieldCache.WaitForSeconds(0.5f);

        gameObject.GetComponent<Animator>().SetTrigger("Start");

        SoundManager.Instance.SFXPlay(clip.Clips[0]);

        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");

        operation.allowSceneActivation = false;

        yield return YieldCache.WaitForSeconds(2.5f);

        operation.allowSceneActivation = true;
    }
}

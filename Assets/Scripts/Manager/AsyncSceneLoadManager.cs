using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneName
{
    MainMenu = 1,
    LevelSelect,
    Play,
    PlayTutorial
}


public class AsyncSceneLoadManager : MonoSingletonManager<AsyncSceneLoadManager>
{
    //Courutine
    [SerializeField] GameObject sceneLoadUI;

    public SceneName nowScene;

    public IEnumerator AsyncSceneLoad(SceneName sceneName)
    {
        Time.timeScale = 0f;

        StartSceneLoad();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName.ToString());

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            sceneLoadUI.GetComponentInChildren<Slider>().value = operation.progress;

            if(operation.progress >= 0.9f)
            {
                break;
            }
            
            yield return null;
        }

        while (true)
        {
            sceneLoadUI.GetComponentInChildren<Slider>().value += Time.unscaledDeltaTime;

            if(sceneLoadUI.GetComponentInChildren<Slider>().value >= 2f)
            {
                operation.allowSceneActivation = true;
                break;
            }

            yield return null;
        }

        nowScene = sceneName;

        Time.timeScale = 1f;

        StartCoroutine( FinishSceneLoad());
    }


    private void StartSceneLoad()
    {
        InputManager.Instance.keyDownAction -= OptionManager.Instance.OptionKeyDown;

        sceneLoadUI.SetActive(true);
        SoundManager.Instance.StopBGM();
    }

    [SerializeField] SoundClip bgms;

    private IEnumerator FinishSceneLoad()
    {
        yield return YieldCache.WaitForSeconds(0.2f);

        switch (nowScene)
        {
            case SceneName.MainMenu:
                SoundManager.Instance.BGMPlay(bgms.Clips[0]);
                break;
            case SceneName.LevelSelect:
                SoundManager.Instance.BGMPlay(bgms.Clips[1]);
                break;
            case SceneName.Play:
                SoundManager.Instance.BGMPlay(bgms.Clips[2]);
                break;
            case SceneName.PlayTutorial:
                SoundManager.Instance.BGMPlay(bgms.Clips[2]);
                break;
        }

        sceneLoadUI.SetActive(false);

        InputManager.Instance.keyDownAction += OptionManager.Instance.OptionKeyDown;
    }

    private void Start()
    {
        SoundManager.Instance.BGMPlay(bgms.Clips[0]);
    }


}

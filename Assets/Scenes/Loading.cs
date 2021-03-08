using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public static Loading instance;

    private void Awake()
    {
        instance = this;
       
    }
    public void LoadingScene(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(Load(sceneIndex));
    }

    IEnumerator Load(int sceneIndex)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            slider.value = operation.progress;
            if (operation.progress == 0.9f)
            {
                slider.value = 1f;
                operation.allowSceneActivation = true;
            }
            yield return null;
            
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

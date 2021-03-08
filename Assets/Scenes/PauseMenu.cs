using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    private InputKeyControl _keyControls;
    public GameObject PausedMenuUI;
 

    private void Awake()
    {
        _keyControls = new InputKeyControl();
       
    }

    private void OnEnable()
    {
        _keyControls.Enable();
    }

    private void OnDisable()
    {
        _keyControls.Disable();
    }
    // Update is called once per frame
    void Start()
    {
        _keyControls.Player.Pause.performed += _ => Paused();
    }

    public void Resume()
    {
        PausedMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Paused()
    {
        if (!isPaused)
        {
            PausedMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            
            Resume();
        }

    }

    public void Menu(int sceneIndex)
    {
        Time.timeScale = 1f;
        Loading.instance.LoadingScene(sceneIndex);
    }

   

}

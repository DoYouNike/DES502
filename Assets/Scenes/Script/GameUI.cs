using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    public GameObject WinUI;
    public GameObject GameOverUI;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win()
    {
        if (GameManager.instance.winGame == true)
        WinUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void GameOver()
    {
        if (!GameManager.instance.winGame)
            GameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}

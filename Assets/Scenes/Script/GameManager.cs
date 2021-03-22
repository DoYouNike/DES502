using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    bool gameEnd;
    private float playerHealth;
    private float enemyHealth;
    private float gameT;
    bool winGame ;
    private void Awake()
    {
        instance = this;
        gameEnd = false;
            }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EndGame();
    }
    public void EndGame()
    {
        playerHealth = PlayerControl.instance.maxH;
        enemyHealth = EnemyControl.instance.maxHealth;
        gameT = BattleTimer.instance.roundTime;
        if (playerHealth <= 0 || enemyHealth <= 0 || gameT <=0)
        {
            gameEnd = true;
            if (playerHealth <= 0 || gameT <=0)
            {
                winGame = false;
                GameUI.instance.GameOver();
            }
            else if (enemyHealth <=0)
            {
                winGame = true;
                GameUI.instance.Win();
            }
        }   
    }

  
}

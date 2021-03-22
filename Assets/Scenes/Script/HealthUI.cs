using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
   
    public Slider eSlider;
    public Slider pSlider;
    public BattleTimer bTimer;
    public Text timer;
   
    // Start is called before the first frame update
    void Start()
    {
       
        eSlider.maxValue = EnemyControl.instance.maxHealth;
        pSlider.maxValue = PlayerControl.instance.maxH;
    }

    // Update is called once per frame
    void Update()
    {

        timer.text = bTimer.roundTime.ToString();
        EnemyHealth();
        PlayerHealth();

    }

    public void EnemyHealth()
    {
        if (EnemyControl.instance.maxHealth < 0)
        {
            EnemyControl.instance.maxHealth = 0;
        }
        
        eSlider.value = EnemyControl.instance.maxHealth;
       
    }
    public void PlayerHealth()
    {
        if (PlayerControl.instance.maxH < 0)
        {
            PlayerControl.instance.maxH = 0;
        }
        pSlider.value = PlayerControl.instance.maxH;
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    Text scoreText;
     public Slider eSlider;
    public Slider pSlider;
    public BattleTimer bTimer;
    public Text timer;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        eSlider.maxValue = EnemyControl.instance.maxH;
        pSlider.maxValue = PlayerControl.instance.maxH;
    }

    // Update is called once per frame
    void Update()
    {

        timer.text = bTimer.roundTime.ToString();
            if (EnemyControl.instance.maxH < 0)
            {
                EnemyControl.instance.maxH = 0;
            }
            scoreText.text = "Health: " + EnemyControl.instance.maxH + "/20";
            eSlider.value = EnemyControl.instance.maxH;
             pSlider.value = PlayerControl.instance.maxH;

    }
}

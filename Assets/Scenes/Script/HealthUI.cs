using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
       if(EnemyControl.instance.maxH >=0)
        scoreText.text = "Health: " + EnemyControl.instance.maxH + "/10";
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTimer : MonoBehaviour
{
    public int roundTime = 100;
    private float lastTimeUpdate = 0;

    private void Start()
    {
        
    }
    private void Update()
    {
        if(roundTime > 0 && Time.time - lastTimeUpdate >1)
        {
            roundTime--;
            lastTimeUpdate = Time.time;
        }
    }
}

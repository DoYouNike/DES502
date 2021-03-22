using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTimer : MonoBehaviour
{
    public static BattleTimer instance;
    public int roundTime = 99;
    private float lastTimeUpdate = 0;

    private void Awake()
    {
        instance = this;
    }
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

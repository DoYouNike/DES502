using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl instance;
    public int maxH;
    Animator enemyAnim;
    public float delay ;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        enemyAnim = GetComponent<Animator>();
        maxH = 10;
        delay = 0f;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (maxH ==0 || maxH<0)
        {
            enemyAnim.SetBool("isDead", true);
            Destroy(this.gameObject, this.enemyAnim.GetCurrentAnimatorStateInfo(0).length+delay);
        }
    }
}
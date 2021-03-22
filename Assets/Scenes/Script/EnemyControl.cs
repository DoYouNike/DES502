using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl instance;
    public int maxHealth;
    Animator enemyAnim;
    public float delay ;
  
    private Rigidbody2D enemyRB;
    public float ms = 2f;
    public float js = 6f;
    public float attack_Distance = 2f;
    private float move_after_attack = 1f;
    private float current_attack_time;
    private float default_attack_time = 2f;

    private bool followPlayer, attackPlayer;
    private bool islight;
    public bool ishit;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        enemyAnim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        maxHealth = 20;
        delay = 0f;
        ishit = false;
        islight = false;
    }
    private void Start()
    {
        followPlayer = true;
        current_attack_time = default_attack_time;
    }
    private void Update()
    {
        if (maxHealth ==0 || maxHealth<0)
        {
            enemyAnim.SetBool("isDead", true);
            Destroy(this.gameObject, this.enemyAnim.GetCurrentAnimatorStateInfo(0).length+delay);
        }
        Move();
        Attack();
    }


    
    private void Move()
    {
        float distance = Vector3.Distance(transform.position,PlayerControl.instance.transform.position);
        if (!followPlayer)
        {
            return;
        }
       
        if(distance > attack_Distance)
        {
          
            Vector3 currentPosition = transform.position;
            currentPosition.x -=  ms * Time.deltaTime;
            transform.position = currentPosition;
            EnemyMove(true);
        }
        else if (distance<= attack_Distance)
        {
            EnemyMove(false);
            followPlayer = false;
            attackPlayer = true;
        }

        //if(distance < 3.0f)
        //{
        //    enemyAnim.SetTrigger("hit");
        //}
    }

    void Attack()
    {
        float distance = Vector3.Distance(transform.position,PlayerControl.instance.transform.position);
        if (!attackPlayer)
            return;
        current_attack_time += Time.deltaTime;
        if(current_attack_time > default_attack_time)
        {
            EnemyAttack(Random.Range(0, 0));
            current_attack_time = 0f;
        }
        if(distance>attack_Distance + move_after_attack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }

    public void EnemyAttack(int attack)
    {
        if(attack == 0)
        {
            enemyAnim.SetTrigger("hit");
            islight = true;
        }
    }
    public void EnemyMove(bool move)
    {
        enemyAnim.SetBool("isWalking", move);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ishit = true;
            AttackTypes();
        }
        else
        {
            ishit = false;
        }
    }

public void AttackTypes()
    {
        if (islight == true)
        {
            PlayerControl.instance.maxH--;
        }
        
    }
}
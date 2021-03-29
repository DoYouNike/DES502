using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl instance;
    public int maxHealth;
    Animator enemyAnim;
    public float delay ;

    public RangeInt _offensivePriority = new RangeInt(1, 3);   //aggressive
    //public RangeInt _assessPriority = new RangeInt(4, 6);      // netural
    public RangeInt _defensivePriority = new RangeInt(4, 6);   //defensive
    public int decideMovePriority;
    private bool assessPlayer;
    public float assessingTime = 3;
    private int moveForward;
    private int moveBackward;
    private int attackpoint;
    private int retreatpoint;
    private int minDecision;
    private int maxDecision;
    private int switchAttack;
    private int switchAttackState;
    public int attackDecision;
    public float attack_Distance = 1.5f;
    private float nextAttack,cooldown;
    private int attackPeriod, attackP;
   


    public int minLightAttackRange, maxLightAttackRange;
    public int minHeavyAttackRange, maxHeavyAttackRange;
    public int minSpecialAttackRange, maxSpecialAttackRange;
    private Rigidbody2D enemyRB;
    public float ms = 1f;
    public float js = 6f;
    public float see_Distance = 2f;
 

   
    private bool islight,isheavy,isspecial;
    public bool ishit;
    private bool isGround;
    [SerializeField]
    Transform groundCheck;
    [SerializeField] private LayerMask ground;
    private ChooseAttack chooseAttack;
    public static OpponentAIState _opponentAIState;
    public enum OpponentAIState
    {
        Initialise,
        AIIdle,
        AIAttack,
        AIMoveToward,
        AIJumpTowardPlayer,
        AIRetreat,
        AIMoveAway,
        AIJumpAwayPlayer,
        AIJump,
        AIDown,
        AILightAttack,
        AIHeavyAttack,
        AISpecialAttack,
        ChooseAttackState,
        WaitForAttackAni

    }

    private enum ChooseAttack
    {
        lightAttack,
        heavyAttack,
        specialAttack
    }
    private void Awake()
    {
        instance = this;
        enemyAnim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        maxHealth = 20;
        delay = 0f;
        ishit = false;
        islight = false;
        isheavy = false;
        isspecial = false;
    }
    private void Start()
    {
        assessPlayer = false;
        minDecision = 1;
        maxDecision = 10;
        retreatpoint = 6;
        decideMovePriority = 0;
        cooldown = 1;
        attackPeriod = 4;
       
        StartCoroutine("AIFSM");
    }
    private void Update()
    {
        if (maxHealth == 0 || maxHealth < 0)
        {
         
            Destroy(this.gameObject, this.enemyAnim.GetCurrentAnimatorStateInfo(0).length + delay);
        }
       

    }
    IEnumerator NumberGen()
    {
        while (true)
        {
            decideMovePriority = Random.Range(1, 6);
            yield return new WaitForSeconds(5);
        }
    }
    private void Initialise()
    {

        decideMovePriority = Random.Range(1, 6);
        attackP = 0;
        nextAttack = 0;
        if (attackDecision == 0)
        {
            attackDecision = 3;
        }

        if (minLightAttackRange != 0)
        {
            minLightAttackRange = 0;
        }

        if (maxLightAttackRange == 0)
        {
            maxLightAttackRange = 1;
        }

        if (minHeavyAttackRange == 0)
        {
            minHeavyAttackRange = 2;
        }

        if (maxHeavyAttackRange == 0)
        {
            maxHeavyAttackRange = 3;
        }

        if (minSpecialAttackRange == 0)
        {
            minSpecialAttackRange = 4;
        }

        if (maxSpecialAttackRange == 0)
        {
            maxSpecialAttackRange = 5;
        }
        _opponentAIState = EnemyControl.OpponentAIState.AIIdle;
    }

    private void AIIdle()
    {
       

        if (decideMovePriority < _defensivePriority.start)
        {
            _opponentAIState = OpponentAIState.AIAttack;
        }

        //if(decideMovePriority > _offensivePriority.end && decideMovePriority < _defensivePriority.start)
        //{
        //    if (assessPlayer == true)
        //        return;

        //    assessPlayer = true;
        //    StartCoroutine("AssessThePlayer");
        //}

        if (decideMovePriority > _offensivePriority.start)
        {
            _opponentAIState = OpponentAIState.AIRetreat;
        }
    }
    private void AIAttack()
    {
        moveForward = Random.Range(1, 10);
        float distance = Vector3.Distance(transform.position, PlayerControl.instance.transform.position);
        if (moveForward >= minDecision && moveForward <= attackpoint)
        {
            _opponentAIState = OpponentAIState.AIMoveToward;
        }
        if (moveForward <= maxDecision && moveForward > attackpoint)
        {
            
            _opponentAIState = OpponentAIState.AIMoveToward;
            
        }
    }
    private void AIMoveToward()
    {
        if (Mathf.Abs(transform.position.x-PlayerControl.instance.transform.position.x ) <= attack_Distance)
        {
            _opponentAIState = OpponentAIState.ChooseAttackState;
        }
        float distance = Vector3.Distance(transform.position, PlayerControl.instance.transform.position);
        Vector3 currentPosition = transform.position;
        currentPosition.x -= ms * Time.deltaTime;
        transform.position = currentPosition;
        EnemyMove(true);
    }

    private void AIJumpTowardPlayer()
    {
        EnemyJump(true);
    }

    private void AIRetreat()
    {
        moveBackward = Random.Range(1, 10);
        Debug.Log("qq");
        if (moveBackward >= minDecision && moveBackward <= attackpoint)
        {
            _opponentAIState = OpponentAIState.AIMoveAway;
          
        }
        if (moveBackward <= maxDecision && moveBackward > attackpoint)
        {
            _opponentAIState = OpponentAIState.AIMoveAway;
         
            
        }
       
    }
    private void AIMoveAway()
    {
        float distance = Vector3.Distance(transform.position, PlayerControl.instance.transform.position);
        Vector3 currentPosition = transform.position;
        currentPosition.x += ms * Time.deltaTime;
        transform.position = currentPosition;
        EnemyMove(true);
        if (distance >= retreatpoint)
        {
            _opponentAIState = OpponentAIState.Initialise;
        }
    }
   
    private void AIJumpAwayPlayer()
    {
        EnemyJump(true);
    }
    private void AIJump()
    {
        if (IsGrounded() == true)
        {
            enemyRB.AddForce(new Vector2(0, js), ForceMode2D.Impulse);
            EnemyJump(true);
            _opponentAIState = OpponentAIState.AIDown;
        }
        

    }
    private void AIDown()
    {
        EnemyJump(false);
        _opponentAIState = OpponentAIState.AIIdle;
    }

    private void ChooseAttackState()
    {
        switchAttack = Random.Range(0, 5);
       
        if(switchAttack >= minLightAttackRange && switchAttack <= maxLightAttackRange)
        {
            switchAttackState = 0;
        }

        if (switchAttack >= minHeavyAttackRange && switchAttack <= maxHeavyAttackRange)
        {
            switchAttackState = 1;
        }

        if (switchAttack >= minSpecialAttackRange && switchAttack <= maxSpecialAttackRange)
        {
            switchAttackState = 2;
        }

        switch(switchAttackState)
        {
            case 0:
                chooseAttack = ChooseAttack.lightAttack;
                break;
            case 1:
                chooseAttack = ChooseAttack.heavyAttack;
                break;
            case 2:
                chooseAttack = ChooseAttack.specialAttack;
                break;
        }
        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }
        else if (nextAttack <= 0)
        {
            if (chooseAttack == ChooseAttack.lightAttack)
            {
                _opponentAIState = OpponentAIState.AILightAttack;
            }

            if (chooseAttack == ChooseAttack.heavyAttack)
            {
                _opponentAIState = OpponentAIState.AIHeavyAttack;
            }

            if (chooseAttack == ChooseAttack.specialAttack)
            {
                _opponentAIState = OpponentAIState.AISpecialAttack;
            }
            nextAttack = cooldown;
            attackP++;
        }
        if(attackP>attackPeriod)
        {
            _opponentAIState = OpponentAIState.Initialise;
        }
    }

    private void WaitForAttackAni()
    {
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack"))
        {
            return;
        }
        if(enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("MediumAttack"))
        {
            return;
        }
        if( enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            return;
        }
            
        _opponentAIState = OpponentAIState.AIIdle;
    }
    private void AILightAttack()
    {
        
        EnemyLightAttack();
        _opponentAIState = OpponentAIState.WaitForAttackAni;
    }
    private void AIHeavyAttack()
    {
    
        EnemyHeavyAttack();
        _opponentAIState = OpponentAIState.WaitForAttackAni;
    }
    private void AISpecialAttack()
    {
      
        EnemySpecialAttack();
        _opponentAIState = OpponentAIState.WaitForAttackAni;
    }
    private IEnumerator AssessThePlayer()
    {
        yield return new WaitForSeconds(assessingTime);
        assessPlayer = false;
    }
    private IEnumerator AIFSM()
    {
        while(true)
        {
            switch(_opponentAIState)
            {
                case OpponentAIState.Initialise:
                    Initialise();
                    break;
                case OpponentAIState.AIIdle:
                    AIIdle();
                    break;
                case OpponentAIState.AIAttack:
                    AIAttack();
                    break;
                case OpponentAIState.AIMoveToward:
                    AIMoveToward();
                    break;
                case OpponentAIState.AIJumpTowardPlayer:
                    AIJumpTowardPlayer();
                    break;
                case OpponentAIState.AIRetreat:
                    AIRetreat();
                    break;
                case OpponentAIState.AIMoveAway:
                    AIMoveAway();
                    break;
                case OpponentAIState.AIJumpAwayPlayer:
                    AIJumpAwayPlayer();
                    break;
                case OpponentAIState.AIJump:
                    AIJump();
                    break;
                case OpponentAIState.AIDown:
                    AIDown();
                    break;
                case OpponentAIState.AILightAttack:
                    AILightAttack();
                    break;
                case OpponentAIState.AIHeavyAttack:
                    AIHeavyAttack();
                    break;
                case OpponentAIState.AISpecialAttack:
                    AISpecialAttack();
                    break;
                case OpponentAIState.ChooseAttackState:
                    ChooseAttackState();
                    break;
                case OpponentAIState.WaitForAttackAni:
                    WaitForAttackAni();
                    break;
              
            }
            yield return null;
        }
    }

    //private void Move()
    //{
    //    float distance = Vector3.Distance(transform.position,PlayerControl.instance.transform.position);
    //    if (!followPlayer)
    //    {
    //        return;
    //    }

    //    if(distance > attack_Distance)
    //    {

    //        Vector3 currentPosition = transform.position;
    //        currentPosition.x -=  ms * Time.deltaTime;
    //        transform.position = currentPosition;
    //        EnemyMove(true);
    //    }
    //    else if (distance<= attack_Distance)
    //    {
    //        EnemyMove(false);
    //        followPlayer = false;
    //        attackPlayer = true;
    //    }

    //    //if(distance < 3.0f)
    //    //{
    //    //    enemyAnim.SetTrigger("hit");
    //    //}
    //}

    //void Attack()
    //{
    //    float distance = Vector3.Distance(transform.position,PlayerControl.instance.transform.position);
    //    if (!attackPlayer)
    //        return;
    //    current_attack_time += Time.deltaTime;
    //    if(current_attack_time > default_attack_time)
    //    {
    //        EnemyAttack(Random.Range(0, 0));
    //        current_attack_time = 0f;
    //    }
    //    if(distance>attack_Distance + move_after_attack)
    //    {
    //        attackPlayer = false;
    //        followPlayer = true;
    //    }
    //}

    public void EnemyLightAttack()
    {

        enemyAnim.SetTrigger("lightAttack");
        islight = true;
        isheavy = false;
        isspecial = false;
    }
    public void EnemyHeavyAttack()
    {
        enemyAnim.SetTrigger("mediumAttack");
        islight = false;
        isheavy = true;
        isspecial = false;

    }
    public void EnemySpecialAttack()
   {
       
            enemyAnim.SetTrigger("specialAttack");
            islight = false;
            isheavy = false;
            isspecial = true;
        

    }

        public void EnemyMove(bool move)
    {
        enemyAnim.SetBool("isWalking", move);
    }

    public void EnemyJump(bool jump)
    {
        enemyAnim.SetBool("isWalking", jump);
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

    private bool IsGrounded()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        return isGround;
    }
    public void AttackTypes()
    {
        if (islight == true)
        {
            PlayerControl.instance.maxH--;
        }
        if (isheavy == true)
        {
            PlayerControl.instance.maxH-=2;
        }
        if (isspecial == true)
        {
            PlayerControl.instance.maxH-=3;
        }

    }
}
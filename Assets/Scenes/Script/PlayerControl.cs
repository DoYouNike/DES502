using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputKeyControl _keyControls;
    public static PlayerControl instance;
    [SerializeField] private float moveS,jumpS;
    [SerializeField] private LayerMask ground;
    private Rigidbody2D rb;
    private bool isLight, isMed, isHeavy, isjumpAttack, isCrouch;
    Animator playerAnim;
    public bool isHit;
    bool isGround;
    private Collider2D col2D;
    public int maxH;
    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float topLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    Transform groundCheck;
    private void Awake()
    {
        instance = this;
        _keyControls = new InputKeyControl();
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        moveS = 5f;
        jumpS = 6f;
        isHit = false;
        isLight = false;
        isMed = false;
        isHeavy = false;
        isjumpAttack = false;
        isCrouch = false;
        maxH = 10;
    }
   
    private void OnEnable()
    {
        _keyControls.Enable();
    }

    private void OnDisable()
    {
        _keyControls.Disable();
    }
    void Start()
    {
        _keyControls.Player.LightAttack.performed += _ => LightAttack();
        _keyControls.Player.MediumAttack.performed += _ => MediumAttack();
        _keyControls.Player.HeavyAttack.performed += _ => HeavyAttack();
        _keyControls.Player.Jump.performed += _ => Jump();
        _keyControls.Player.Crouch.performed += _ => Crouch();
    }

    private void Crouch()
    {
        float valueP = _keyControls.Player.Crouch.ReadValue<float>();
        if (valueP > 0)
        {
            isCrouch = true;
            playerAnim.SetBool("isCrouch", true);
            
        }
        else if (valueP == 0)
        {
            isCrouch = false;
            playerAnim.SetBool("isCrouch", false);
         
        }
    }

    private void Jump()
    {

        float valuez = _keyControls.Player.Jump.ReadValue<float>();
        if ( valuez >0)
        {
            if (IsGrounded() == true)
            {
                rb.AddForce(new Vector2(0, jumpS), ForceMode2D.Impulse);
                isjumpAttack = true;
                playerAnim.SetBool("isJump", true);
                
            }
           
        }
       else if (valuez ==0)
        {
            isjumpAttack = false;
            playerAnim.SetBool("isJump", false);
        }
    }

    private bool IsGrounded()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        return isGround;
    }

    private void LightAttack()
    {
        if (IsGrounded()&& isCrouch == false&&!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Mila_Punch"))
        {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetTrigger("hit");
        }
        else if(isjumpAttack == true &&!IsGrounded()&& !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Mila_JumpKick"))
        {
            playerAnim.SetTrigger("jumpKick");
        }
        else if (IsGrounded()&&isCrouch == true && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Mila_CrouchKick"))
        {
            playerAnim.SetTrigger("crouchKick");
        }
        isLight = true;
        isMed = false;
        isHeavy = false;
    }
    private void MediumAttack()
    {
        if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Mila_Punch"))
        {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetTrigger("hit");
        }
        isMed = true;
        isLight = false;
        isHeavy = false;

    }

    private void HeavyAttack()
    {
        if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Mila_Punch"))
        {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetTrigger("hit");
        }
        isHeavy = true;
        isLight = false;
        isMed = false;
       
    }

    void Update()
    {
        // Read the Move value
        float moveV = _keyControls.Player.Move.ReadValue<float>();
        //Move the Player
        Vector3 currentPosition = transform.position;
        currentPosition.x += moveV * moveS * Time.deltaTime;
        transform.position = currentPosition;
        if (moveV != 0 && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Mila_Punch") && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Mila_Jump") && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Mila_Crouch"))
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            isHit = true;
            AttackType();
        }
        else
        {
            isHit = false;
        }
    }

    private void AttackType()
    {
        if (isLight == true)
        {
            EnemyControl.instance.maxH--;
        }
        else if (isMed == true)
        {
            EnemyControl.instance.maxH-=2;
        }
        else if (isHeavy == true)
        {
            EnemyControl.instance.maxH-=3;
        }
    }

}

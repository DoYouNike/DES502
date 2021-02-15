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
    Animator playerAnim;
    public bool isHit;
    bool isCrouch;
    private Collider2D col2D;
    public int maxH;
    private void Awake()
    {
        instance = this;
        _keyControls = new InputKeyControl();
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        moveS = 5f;
        jumpS = 5f;
        isCrouch = false;
        isHit = false;
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
        _keyControls.Player.Jump.performed += _ => Jump();
        _keyControls.Player.Crouch.performed += _ => Crouch();
    }

    private void Crouch()
    {

        playerAnim.SetBool("isCrouch", true);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpS), ForceMode2D.Impulse); 
        }  
    }

    private bool IsGrounded()
    {
        Vector2 topLeftP = transform.position;
        topLeftP.x -= col2D.bounds.extents.x;
        topLeftP.y += col2D.bounds.extents.y;

        Vector2 bottomRightP = transform.position;
        bottomRightP.x += col2D.bounds.extents.x;
        bottomRightP.y -= col2D.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftP,bottomRightP, ground);
    }

    private void LightAttack()
    {
        if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetTrigger("hit");
        }
    }

    void Update()
    {
        // Read the Move value
        float moveV = _keyControls.Player.Move.ReadValue<float>();
        //Move the Player
        Vector3 currentPosition = transform.position;
        currentPosition.x += moveV * moveS * Time.deltaTime;
        transform.position = currentPosition;
        if (moveV != 0 && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
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
        if (collision.gameObject.name.Equals("Enemy"))
        {
            isHit = true;
            EnemyControl.instance.maxH--;
        }
        else
        {
            isHit = false;
        }
    }
}

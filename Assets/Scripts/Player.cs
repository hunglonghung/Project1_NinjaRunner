using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : PlayerInfo
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] Animator anim;
    [SerializeField] float multiplier = 1.1f;
    [SerializeField] float speed = 1.0f;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] Kunai kunaiPrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] GameObject attackArea;
    string currentAnimName;
    private bool isGrounded = false ;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDead = false;
    private int coinCount = 0;
    float horizontal;
    Vector3 savePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
        OnInit();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        if(isDead)
        {
            return;
        }
        isGrounded = checkGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(isAttack);
        if(isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if(isGrounded)
        {
            Debug.Log(isDead);
            if(isDead)
            {
                return;
            }
            if (isJumping)
            {
                return;
            }

            // jump
            if (Input.GetKeyDown(KeyCode.K) && isGrounded)
            {
                isJumping = true;
                changeAnim("Jump");
                rb.AddForce(jumpForce * Vector2.up) ;
            }
                if (Mathf.Abs(horizontal) > 0.1f)
                {
                    changeAnim("Run");
                }
            //attack
            if(Input.GetKeyDown(KeyCode.J) && isGrounded)
            {
                Debug.Log("Attack!");
                Attack();
                Debug.Log("Done");
            }

            //throw
            if(Input.GetKeyDown(KeyCode.U) && isGrounded)
            {
                Throw();
            }
            

            //change anim run
           
            Debug.Log(isJumping);
        }  
        //fall
        if (!isGrounded && rb. velocity.y < 0)
        {
            changeAnim("Fall");
            isJumping = false;
        }
        //run
        if(Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * Time.deltaTime * speed,rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
            
        }
        //idle
        else if(isGrounded)
        {
            changeAnim("Idle");
            rb.velocity = Vector2.zero;
        }
        

    }
    bool checkGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * multiplier, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,multiplier,groundLayer);
        return hit.collider != null;

    }
    void Attack()
    {
        changeAnim("Attack");
        isAttack = true;
        Invoke("ResetAttack",0.5f);
        attackArea.SetActive(true);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack),0.5f);
    }
    void Throw()
    {
        changeAnim("Throw");
        isAttack = true;
        
        Instantiate(kunaiPrefab,throwPoint.position,throwPoint.rotation);
        Debug.Log(Instantiate(kunaiPrefab,throwPoint.position,throwPoint.rotation));
        Invoke("ResetAttack",0.5f);
    }
    void ResetAttack()
    {
        Debug.Log("Called");
        isAttack = false;
        changeAnim("idle");
    }
    void changeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Coin")
        {
            Destroy(other.gameObject);
            coinCount ++;   
        }
        if(other.tag == "DeathZone")
        {
            Debug.Log("Dead!");
            isDead = true;
            changeAnim("Dead");
            Invoke(nameof(OnInit),1f);
            
        }
    }
    public void SavePoint()
    {
        savePoint = transform.position;
    }
    public override void OnInit()
    {
        base.OnInit(); // Gọi OnInit từ PlayerInfo để reset hp
        isDead = false;
        isDead2 = false;
        isAttack = false;
        transform.position = savePoint;
        changeAnim("Idle");
        SavePoint();
        DeActiveAttack();
        
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    public override void OnDeath()
    {
        base.OnDeath();
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
}

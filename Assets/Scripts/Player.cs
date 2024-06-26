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
    [SerializeField] float multiplier = 1.1f;
    [SerializeField] float speed = 1.0f;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] Kunai kunaiPrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] GameObject attackArea;
    
    [SerializeField]private bool isGrounded = false ;
    [SerializeField]private int coin = 0;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDead = false;
    private int coinCount = 0;
    float horizontal;
    Vector3 savePoint;
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        OnInit();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        if(isDead || isDead2)
        {
            return;
        }
        isGrounded = checkGrounded();
        // horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(isAttack);
        if(isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if(isGrounded)
        {
            Debug.Log(isDead);
            if(isDead || isDead2)
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
                Jump();
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
        else if(isGrounded && !isDead2)
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
    public void Attack()
    {
        if(isGrounded && !isAttack)
        {
            changeAnim("Attack");
            isAttack = true;
            Invoke("ResetAttack",0.5f);
            attackArea.SetActive(true);
            ActiveAttack();
            Invoke(nameof(DeActiveAttack),0.5f);
        }
        
    }
    public void Throw()
    {
        if(isGrounded && !isAttack)
        {
            changeAnim("Throw");
            isAttack = true;
            
            Instantiate(kunaiPrefab,throwPoint.position,throwPoint.rotation);
            
            Invoke("ResetAttack",0.5f);
        }
        
    }
    public void Jump()
    {
        isJumping = true;
        changeAnim("Jump");
        rb.AddForce(jumpForce * Vector2.up) ;
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
            coin ++;   
            PlayerPrefs.SetInt("coin",coin);
            UiManager.Instance.SetCoin(coin);
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
        isAttack = false;
        transform.position = savePoint;
        changeAnim("Idle");
        SavePoint();
        DeActiveAttack();
        UiManager.Instance.SetCoin(coin);
        
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
    public void SetMove(float Horizontal)
    {
        this.horizontal = Horizontal;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerInfo
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] GameObject attackArea;
    [SerializeField] public bool isAttacking;
    private IState currentState;
    private PlayerInfo target;
    public PlayerInfo Target => target;
    
    public bool isRight = true;
    // Start is called before the first frame update
    void Start()
    {
        base.OnInit();
        changeState(new IdleState());
    }
    internal void SetTarget(PlayerInfo player)
    {
        this.target = player;
        Debug.Log(target);
        if(targetInRange())
        {
            Debug.Log("Attack Player!");
            changeState(new attackState());
            
            return;
        }
        else{
            if(target != null)
            {
                changeState(new PatrolState());
            }
            else
            {
                changeState(new IdleState());
            }
        }
    }

    // Update is called once per frame
    private void Update()
{
    if (isDead2 || currentState == null) return;
    
    Debug.Log(currentState);
    currentState.OnExecute(this);
}

    public override void OnInit()
    {
        base.OnInit();
        changeState(new IdleState());
        DeActiveAttack();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
    public override void OnDeath()
    {
        changeState(null);
        rb.velocity = Vector2.zero;
        base.OnDeath();
    }
    public void changeState(IState Newstate)
        {
            if(currentState != null) {
            currentState.OnExit(this);
            }
            currentState = Newstate;
            if(Newstate != null)
            {
                currentState.OnEnter(this);
            }
        }
    
    public void Moving()
    {
        changeAnim("Run");
        rb.velocity = transform.right * moveSpeed;
    }
    public void StopMoving()
    {
        changeAnim("Idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ActiveAttack();
        changeAnim("Attack");
        Invoke(nameof(DeActiveAttack), .5f);
    }
    public bool targetInRange()
    {
        if(target == null) return false;    
        if (target != null && Vector2.Distance(target.transform.position, transform.position) < attackRange) return true;
        return Vector2.Distance(target.transform.position, transform.position) <= attackRange;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyWall")
        {

            changeDirection(!isRight);
        }
    }
    public void changeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion. Euler(Vector3.zero) : Quaternion. Euler(Vector3.up * 180);
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


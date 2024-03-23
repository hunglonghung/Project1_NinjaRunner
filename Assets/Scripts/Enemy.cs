using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerInfo
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
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
        if(targetInRange())
        {
            changeState(new attackState());
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
        Debug.Log(currentState);
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        Debug.Log("OnInit");
        base.OnInit();
        changeState(new IdleState());
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    public override void OnDeath()
    {
        base.OnDeath();
    }
    public void changeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if(currentState != null)
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
        rb.velocity = Vector2.zero;
        changeAnim("Idle");
    }
    public void Attack()
    {
        changeAnim("Attack");
    }
    public bool targetInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) < attackRange) return true;
        return Vector2.Distance(target.transform.position, transform.position) <= attackRange;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyWall")
        {
            Debug.Log("collided!");
            changeDirection(!isRight);
        }
    }
    public void changeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion. Euler(Vector3.zero) : Quaternion. Euler(Vector3.up * 180);
    }
}


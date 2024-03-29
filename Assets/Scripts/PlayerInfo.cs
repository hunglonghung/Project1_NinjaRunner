using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] private CombatText combatTextPrefab;
    public string currentAnimName;
    [SerializeField] public float hp = 100f;
    [SerializeField] public bool isDead2 => hp <= 0;
    public void Start()
    {
        OnInit();
    }
    protected void changeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100,transform);
    }
    public virtual void OnDespawn()
    {

    }
    public void OnHit(float damage)
    {
        if(hp >= damage)
        {
            hp -= damage;
            if(hp <= damage)
            {
                hp = 0;
                
                OnDeath();  
            }
            healthBar.setNewHp(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up,Quaternion.identity).OnInit(damage);
        }
    }
    public virtual void OnDeath()
    {
        changeAnim("Dead");
        Debug.Log("Dead!!!!");
        Invoke(nameof(OnDespawn),2f);
    }
    
}
    


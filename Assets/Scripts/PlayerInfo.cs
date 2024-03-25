using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] Animator anim2;
    string currentAnimName2;
    [SerializeField] public float hp = 100f;
    [SerializeField] public bool isDead2 = false;
    public void changeAnim(string animName)
    {
        if(currentAnimName2 != animName)
        {
            anim2.ResetTrigger(animName);
            currentAnimName2 = animName;
            anim2.SetTrigger(currentAnimName2);
        }
    }
    public virtual void OnInit()
    {
        hp = 100;
    }
    public virtual void OnDespawn()
    {
        OnInit();
    }
    public void OnHit(float damage)
    {
        Debug.Log("Hit");
            hp -= damage;
            if(hp <= 0) isDead2= true;
            if (isDead2)
            {
                OnDeath();
            }
            
        
            
    }
    public virtual void OnDeath()
    {
        changeAnim("Dead");
        Invoke(nameof(OnDespawn),2f);
    }
    
}
    


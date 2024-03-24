using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] Animator anim2;
    string currentAnimName2;
    private float hp;
    public bool isDead2 => hp <= 0;
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
        
    }
    public void OnHit(float damage)
    {
        Debug.Log(hp);
        if (hp >= damage)
        {
            hp -= damage;
            if (isDead2)
            {
                OnDeath();
            }
            
        }
            
    }
    public virtual void OnDeath()
    {
        changeAnim("Dead");
        Invoke(nameof(OnDespawn),2f);
    }
    
}
    


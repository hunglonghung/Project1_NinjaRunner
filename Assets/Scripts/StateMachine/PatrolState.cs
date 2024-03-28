using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer, randomtime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomtime = Random.Range(3f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(enemy.Target != null)
        {//changes the enemy's direction to the player's direction
            enemy.changeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            if (enemy.targetInRange())//When the target is within range, attack and Stop moving
            {
                enemy.changeState(new attackState());

            }
            else
            {
                enemy.Moving();
            }
            
        }
        else
        {
            if (timer < randomtime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.changeState(new IdleState());
            }
        }
        
        
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
;
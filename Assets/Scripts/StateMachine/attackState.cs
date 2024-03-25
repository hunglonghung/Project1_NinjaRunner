using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;

public class attackState : IState
{
    float timer;
    public void OnEnter(Enemy enemy )
    {
        if(enemy.Target != null)
        {
            enemy.changeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();
            enemy.Attack(); 
            
        }
        else
        {
            enemy.changeState(new PatrolState());
        }
        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(timer >= 1.5f)
        {
            enemy.changeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        Debug.Log("Attack Exit");
    }

}

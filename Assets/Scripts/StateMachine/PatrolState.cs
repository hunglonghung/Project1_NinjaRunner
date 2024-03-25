using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PatrolState : IState
{
    float randomTime;
    float timer;

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(3f,6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(enemy.Target != null)
        {
            enemy.changeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            if(enemy.targetInRange())
            {
                enemy.changeState(new attackState());
            }
            else 
            {
                Debug.Log("Lost Target!");
                enemy.Moving();
            }
            if(timer < randomTime)
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
        Debug.Log("Patrol Exit");
    }
}

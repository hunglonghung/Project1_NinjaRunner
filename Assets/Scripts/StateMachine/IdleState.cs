using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float randomTime;
    float timer;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(2f,4f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(timer > randomTime)
        {
            enemy.changeState(new PatrolState());

        }
        

    }

    public void OnExit(Enemy enemy)
    {
        Debug.Log("Idle exit");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState :IState
{
    // Start is called before the first frame update
    float Randtime;
    float timer;
    public void OnEnter(BOT bot)
    {
        timer = 0;
        Randtime = Random.Range(5, 8f);
      
    }
    public void OnExcute(BOT bot)
    {
        timer += Time.deltaTime;
   
        if (timer < Randtime)
        {
            bot.Attack();
        }
        else
        {
            bot.ChangeState(new IncreaState());
        }

    }
    public void OnExit(BOT bot) { }
}

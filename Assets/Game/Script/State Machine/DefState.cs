using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefState : IState
{
    float Randtime;
    float timer;
    public void OnEnter(BOT bot)
    {
        timer = 0;
        Randtime = Random.Range(3f,5f);
    }
    public void OnExcute(BOT bot)
    {
        timer += Time.deltaTime;
        if(timer < Randtime)
        {
            bot.Def();
        }
        else
        {
            bot.ChangeState(new IncreaState());
        }
        
    }
    public void OnExit(BOT bot) { }
}

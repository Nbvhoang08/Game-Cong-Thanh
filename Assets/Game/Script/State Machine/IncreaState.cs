using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  IncreaState :  IState
{
   
    // Start is called before the first frame update
     float Randtime;
     float timer;
    public void OnEnter(BOT bot)
    {
        timer = 0;
        Randtime = Random.Range(1f, 3f);
    
    }
   public void OnExcute(BOT bot)
    {
      
        if (bot.CurrentNode._soldiers > 10)
        {
            
            timer += Time.deltaTime;
            if (timer > Randtime)
            {
                if (bot.attack)
                {
                    bot.ChangeState(new AttackState());
                }
                else
                {
                    bot.ChangeState(new DefState());
                }
                
            }

        }
        
       
    }
    public void OnExit(BOT bot) { }
}

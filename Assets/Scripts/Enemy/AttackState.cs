using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    float attakDelayTime = 4;
    public AttackState(StateManager sm) : base(sm)
    {
    }

    public override void Entry()
    {
        base.Entry();
        currTime = attakDelayTime;
    }

    public override void Update()
    {
        if(IsDelayComplete(attakDelayTime))
        {
            if(DecideStateByDist() == StateManager.EEnemyState.ATTACK)
            {
                anim.SetTrigger("ATTACK");
                //anim.CrossFade("ATTACK", 0.05f, 0, 0);
            }
        }
    }
}

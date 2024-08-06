using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class IdleState : BaseState
{
    bool isFirst = true;
    public IdleState(StateManager sm) : base(sm)
    {
    }

    public override void Entry()
    {
        base.Entry();
        if(isFirst)
        {
            isFirst = false;
        }
        else
        {
            anim.SetTrigger("IDLE");
        }
    }

    public override void Update()
    {
        base.Update();

        if(DecideStateByDist() == StateManager.EEnemyState.IDLE)
        {
            if(IsDelayComplete(2))
            {
                stateMgr.ChangeState(StateManager.EEnemyState.PATROL);
            }
        }
    }
}

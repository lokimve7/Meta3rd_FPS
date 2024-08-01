using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DamageState : BaseState
{
    // 피격 대기 시간
    float damageDelay = 4;
    public DamageState(StateManager sm) : base(sm)
    {
        
    }

    public override void Entry()
    {
        base.Entry();
        
        hpSystem.UpdateHP(-1);
        anim.SetTrigger("DAMAGE");
    }

    public override void Update()
    {
        if (IsDelayComplete(damageDelay))
        {
            DecideStateByDist();            
        }
    }
}

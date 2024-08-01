using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BaseState
{
    public DieState(StateManager sm) : base(sm)
    {
    }

    public override void Entry()
    {
        base.Entry();

        CapsuleCollider coll = stateMgr.GetComponent<CapsuleCollider>();
        coll.enabled = false;
        anim.SetTrigger("DIE");
    }
}

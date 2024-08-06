using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    public MoveState(StateManager sm) : base(sm)
    {
    }

    public override void Entry()
    {
        base.Entry();
        anim.SetTrigger("MOVE");
        nav.isStopped = false;
    }

    public override void Update()
    {
        base.Update();

        if(DecideStateByDist() == StateManager.EEnemyState.MOVE)
        {
            nav.SetDestination(stateMgr.player.transform.position);

            //// Player 를 향하는 방향 구하자.
            //Vector3 dir = player.transform.position - stateMgr.transform.position;
            //dir.y = 0;
            //dir.Normalize();
            //// 나의 앞방향을 dir 로 하자.
            //stateMgr.transform.forward = dir;
            //// 그 방향으로 이동하자.
            //stateMgr.transform.position += dir * moveSpeed * Time.deltaTime;
        }       
    }
}

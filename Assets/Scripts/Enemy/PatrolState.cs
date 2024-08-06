using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    public PatrolState(StateManager sm) : base(sm)
    {
    }

    public override void Entry()
    {
        base.Entry();

        nav.isStopped = false;
        anim.SetTrigger("MOVE");
        SetRandomPos();
    }

    public override void Update()
    {
        base.Update();

        if(DecideStateByDist() == StateManager.EEnemyState.PATROL)
        {
            if(nav.velocity.sqrMagnitude > 0)
            {
                stateMgr.transform.forward = nav.velocity.normalized;
            }

            if(nav.remainingDistance <= 0.1f)
            {
                stateMgr.ChangeState(StateManager.EEnemyState.IDLE);
            }
        }
    }

    void SetRandomPos()
    {   
        Vector2 randDir = Random.insideUnitCircle;
        Vector3 randPos = stateMgr.transform.position + new Vector3(randDir.x, 0, randDir.y) * Random.Range(1.0f, 2.0f);
        randPos.y = 50;
        Ray ray = new Ray(randPos, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            NavMeshPath path = new NavMeshPath();
            if(nav.CalculatePath(hit.point, path))
            {
                nav.SetDestination(hit.point);
            }
            else
            {
                Debug.LogError("길을 찾을 수 없는 위치 입니다.");
                //SetRandomPos();
            }
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseState 
{
    protected StateManager stateMgr;
       

    protected Animator anim;

    protected float currTime;

    protected HPSystem hpSystem;

    protected NavMeshAgent nav;

    public BaseState(StateManager sm)
    {
        stateMgr = sm;
        anim = sm.GetComponentInChildren<Animator>();
        nav = sm.GetComponent<NavMeshAgent>();

        hpSystem = sm.GetComponent<HPSystem>();
        // 가져온 컴포넌트에서 OnDie 함수를 등록
        hpSystem.onDie = stateMgr.OnDie;
    }
    public virtual void Entry()
    {
        currTime = 0;
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }

    protected StateManager.EEnemyState DecideStateByDist()
    {
        return stateMgr.DecideStateByDist();        
    }

    protected bool IsDelayComplete(float delayTime)
    {
        // 시간을 증가 시키자.
        currTime += Time.deltaTime;
        // 만약에 시간이 delayTime 보다 커지면
        if (currTime >= delayTime)
        {
            // 현재시간 초기화
            currTime = 0;
            // true 반환
            return true;
        }

        // 그렇지 않으면       
        // false 반환
        return false;
    }    
}

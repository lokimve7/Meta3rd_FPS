using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class BaseState 
{
    protected StateManager stateMgr;

    // 플레이어 게임오젝트
    protected GameObject player;

    // 인지 범위
    protected float traceRange = 8;
    // 공격 범위
    protected float attakRange = 2;
    // 이동 속력
    protected float moveSpeed = 3;

    protected Animator anim;

    protected float currTime;

    protected HPSystem hpSystem;

    public BaseState(StateManager sm)
    {
        stateMgr = sm;
        player = GameObject.Find("Player");
        anim = sm.GetComponentInChildren<Animator>();

        hpSystem = sm.GetComponent<HPSystem>();
        // 가져온 컴포넌트에서 OnDie 함수를 등록
        hpSystem.onDie = OnDie;
    }
    public virtual void Entry()
    {
        currTime = 0;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }

    protected StateManager.EEnemyState DecideStateByDist()
    {
        // 만약에 Player 와 거리가 attakRange 보다 작으면
        float dist = Vector3.Distance(
            player.transform.position,
            stateMgr.transform.position);
        if (dist < attakRange)
        {
            stateMgr.ChangeState(StateManager.EEnemyState.ATTACK);
        }
        // 그렇지 않고 인지범위 보다 작으면
        else if (dist < traceRange)
        {
            // 이동 상태로 전환
            stateMgr.ChangeState(StateManager.EEnemyState.MOVE);
        }
        // 그렇지 않고 인지범위 보다 크면
        else
        {
            // 대기상태로 전환
            stateMgr.ChangeState(StateManager.EEnemyState.IDLE);
        }
        return stateMgr.GetCurrState;
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

    void OnDie()
    {
        stateMgr.ChangeState(StateManager.EEnemyState.DIE);
    }
}

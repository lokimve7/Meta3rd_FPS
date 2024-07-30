using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy : MonoBehaviour, IAnimationInterface
{
    // 상태 enum (열거형)
    public enum EEnemyState
    {
        IDLE,
        MOVE,
        ATTACK,
        ATTACK_DELAY,
        DAMAGE,
        DIE
    }

    // 현재 상태   
    public EEnemyState currState;

    // 플레이어 게임오젝트
    GameObject player;

    // 인지 범위
    public float traceRange = 8;
    // 공격 범위
    public float attakRange = 2;

    // 이동 속력
    public float moveSpeed = 3;

    // 현재 시간
    float currTime;
    // 공격 Delay 시간
    public float attakDelayTime = 2;

    Animator anim;

    void Start()
    {
        // Player 어 찾아오자.
        player = GameObject.Find("Player");

        anim = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        // switch / case
        switch (currState)
        {
            case EEnemyState.IDLE:
                UpdateIdle();
                break;

            case EEnemyState.MOVE:
                UpdateMove();
                break;

            case EEnemyState.ATTACK:
                Attack();
                break;
            case EEnemyState.ATTACK_DELAY:
                UpdateAttackDelay();
                break;

            case EEnemyState.DAMAGE:
                break;

            case EEnemyState.DIE:
                break;
        }
    }
    

    // 상태가 전환 될 때 한번만 실행하는 동작
    void ChangeState(EEnemyState state)
    {
        // 이전 상태 -> 현재 상태
        print(currState + " ----> " + state);

        // 현재 상태를 state 값으로 설정
        currState = state;

        // 현재시간을 초기화
        currTime = 0;
        
        switch(currState)
        {
            case EEnemyState.ATTACK:
            case EEnemyState.IDLE:
            case EEnemyState.MOVE:
                anim.SetTrigger(currState.ToString());
                break;            
        }
    }

    // 대기 상태일때 계속 해야 하는 동작
    void UpdateIdle()
    {
        // Player 와 나의 거리를 구하자.
        float dist = Vector3.Distance(player.transform.position, transform.position);
        // 만약에 그 거리가 인지범위 보다 작으면
        if(dist < traceRange)
        {
            // MOVE 상태로 전환

            ChangeState(EEnemyState.MOVE);
        }
    }

    // 이동 상태일때 계속 해야 하는 동작
    void UpdateMove()
    {
        // Player 와의 거리를 구하자.
        float dist = Vector3.Distance(player.transform.position, transform.position);
        // 그 거리가 공격범위 보다 작으면
        if (dist < attakRange)
        {
            // 상태를 공격상태로 전환
            ChangeState(EEnemyState.ATTACK);

        }
        else if (dist > traceRange)
        {
            ChangeState(EEnemyState.IDLE);
        }
        // 그렇지 않으면
        else
        {
            // Player 를 향하는 방향 구하자.
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            // 나의 앞방향을 dir 로 하자.
            transform.forward = dir;
            // 그 방향으로 이동하자.
            transform.position += dir * moveSpeed * Time.deltaTime;
        }        
    }

    // 공격 상태일때 계속 해야 하는 동작
    void Attack()
    {
        // 플레어를 공격하자.
        print("공격! 공격!");
        ChangeState(EEnemyState.ATTACK_DELAY);
    }

    private void UpdateAttackDelay()
    {
        // 시간을 흐르게 하자.
        currTime += Time.deltaTime;
        // 공격 Delay 시간만큼 기다렸다가
        if (currTime > attakDelayTime)
        {
            // 현재 시간 초기화
            currTime = 0;
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if(dist < attakRange)
            {
                ChangeState(EEnemyState.ATTACK);
            }
            else if(dist < traceRange)
            {
                ChangeState(EEnemyState.MOVE);
            }
            else
            {
                ChangeState(EEnemyState.IDLE);
            }
        }
    }

    public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("IDLE"))
        {
            Debug.Log("IDLE - Enter");
        }
        else if (stateInfo.IsName("MOVE"))
        {
            Debug.Log("MOVE - Enter");
        }
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("IDLE"))
        {
            Debug.Log("IDLE - Exit");
        }
        else if (stateInfo.IsName("MOVE"))
        {
            Debug.Log("MOVE - Exit");
        }
    }
}

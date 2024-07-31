﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 상태 enum (열거형)
    public enum EEnemyState
    {
        IDLE,
        MOVE,
        ATTACK,
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

   

    void Start()
    {
        // Player 어 찾아오자.
        player = GameObject.Find("Player");
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
                UpdateAttack();
                break;

            case EEnemyState.DAMAGE:
                UpdateDamage();
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
                currTime = attakDelayTime;
                break;
            case EEnemyState.DAMAGE:
                { 
                    HPSystem hpSystem = GetComponent<HPSystem>();
                    hpSystem.UpdateHP(-1);
                }
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
        if(dist < attakRange)
        {
            // 상태를 공격상태로 전환
            ChangeState(EEnemyState.ATTACK);
            
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
    void UpdateAttack()
    {        
        // 공격 Delay 시간만큼 기다렸다가
        if(IsDelayComplete(attakDelayTime))
        {
            // 만약에 Player 와 거리가 attakRange 보다 작으면
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < attakRange)
            {
                // 플레어를 공격하자.
                print("공격! 공격!");               
            }
            // 그렇지 않고 인지범위 보다 작으면
            else if (dist < traceRange)
            {
                // 이동 상태로 전환
                ChangeState(EEnemyState.MOVE);
            }
            // 그렇지 않고 인지범위 보다 크면
            else
            {
                // 대기상태로 전환
                ChangeState(EEnemyState.IDLE);
            }
        }
    }


    // 피격 대기 시간
    public float damageDelay = 1;
    void UpdateDamage()
    {
        // 피격 시간 만큼 기다렸다가        
        if(IsDelayComplete(damageDelay))
        {
            // 나의 행동을 결정하자.
            // 만약에 Player 와 거리가 attakRange 보다 작으면
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < attakRange)
            {
                // 공격상태로 전환
                ChangeState(EEnemyState.ATTACK);
            }
            // 그렇지 않고 인지범위 보다 작으면
            else if (dist < traceRange)
            {
                // 이동 상태로 전환
                ChangeState(EEnemyState.MOVE);
            }
            // 그렇지 않고 인지범위 보다 크면
            else
            {
                // 대기상태로 전환
                ChangeState(EEnemyState.IDLE);
            }
        }
    }

    public void OnDamaged()
    {
        // 상태를 Damage 로 전환
        ChangeState(EEnemyState.DAMAGE);
    }

    bool IsDelayComplete(float delayTime)
    {
        // 시간을 증가 시키자.
        currTime += Time.deltaTime;
        // 만약에 시간이 delayTime 보다 커지면
        if(currTime >= delayTime)
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

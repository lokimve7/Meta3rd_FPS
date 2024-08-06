using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // 상태 enum (열거형)
    public enum EEnemyState
    {
        NONE = -1,
        IDLE,
        PATROL,
        MOVE,
        ATTACK,
        ATTACK_DELAY,
        DAMAGE,
        DIE,
        MAX
    }
    

    BaseState[] allState;
    BaseState currStateClass;

    [SerializeField]
    EEnemyState currState = EEnemyState.NONE;

    // 플레이어 게임오젝트
    public GameObject player;

    // 인지 범위
    float traceRange = 8;
    // 공격 범위
    float attakRange = 2;
    // 이동 속력
    float moveSpeed = 3;

    public EEnemyState GetCurrState { get { return currState; } }
        

    void Start()
    {
        player = GameObject.Find("Player");

        allState = new BaseState[(int)EEnemyState.MAX];
        allState[(int)EEnemyState.IDLE] = new IdleState(this);
        allState[(int)EEnemyState.PATROL] = new PatrolState(this);
        allState[(int)EEnemyState.MOVE] = new MoveState(this);
        allState[(int)EEnemyState.ATTACK] = new AttackState(this);
        allState[(int)EEnemyState.DAMAGE] = new DamageState(this);
        allState[(int)EEnemyState.DIE] = new DieState(this);

        ChangeState(EEnemyState.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        currStateClass.Update();
    }
    public void ChangeState(EEnemyState state, bool changeForce = false)
    {
        if (changeForce == false &&currState == state) return;

        // 이전 상태 -> 현재 상태
        print(currState + " ----> " + state);

        // 현재 상태를 state 값으로 설정
        currState = state;

        if(currStateClass != null) currStateClass.Exit();
        currStateClass = allState[(int)state];
        currStateClass.Entry();
    }

    public EEnemyState DecideStateByDist()
    {
        // 만약에 Player 와 거리가 attakRange 보다 작으면
        float dist = Vector3.Distance(
            player.transform.position,
            transform.position);
        if (dist < attakRange)
        {
            ChangeState(EEnemyState.ATTACK);
        }
        // 그렇지 않고 인지범위 보다 작으면
        else if (dist < traceRange)
        {
            // 이동 상태로 전환
            ChangeState(EEnemyState.MOVE);
        }
        // 그렇지 않고 인지범위 보다 크면
        else if (GetCurrState != EEnemyState.PATROL)
        {
            // 대기상태로 전환
            ChangeState(EEnemyState.IDLE);
        }
        return GetCurrState;
    }

    public void OnDamaged()
    {
        // 상태를 Damage 로 전환
        ChangeState(EEnemyState.DAMAGE, true);
    }

    public void OnDie()
    {
        ChangeState(EEnemyState.DIE);
    }
}

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
        MOVE,
        ATTACK,
        ATTACK_DELAY,
        DAMAGE,
        DIE,
        MAX
    }

    BaseState[] allState;
    BaseState currStateClass;

    EEnemyState currState = EEnemyState.NONE;
    public EEnemyState GetCurrState { get { return currState; } }
        

    void Start()
    {
        allState = new BaseState[(int)EEnemyState.MAX];
        allState[(int)EEnemyState.IDLE] = new IdleState(this);
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

    public void OnDamaged()
    {
        // 상태를 Damage 로 전환
        ChangeState(EEnemyState.DAMAGE, true);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// delegate : 함수를 담을 수 있는 자료형
delegate void DelgateTest(int a, int b);    
delegate void DelgateTes2t(float a, float b);    

public class HPSystem : MonoBehaviour
{
    // 최대 HP
    public float maxHP = 3;
    // 현재 HP
    float currHP;

    // HP가 0 이 되었을 때 호출되는 함수를 담을 변수
    public Action onDie;

    void Start()
    {
        // 현재 HP 를 최대 HP 로 설정
        currHP = maxHP;
    }

    void Update()
    {
        
    }

    public void UpdateHP(float value)
    {
        // 현재 HP 를 value 만큼 더하자.
        currHP += value;
        // 만약에 현재 HP 가 0보다 작거나 같으면
        if(currHP <= 0)
        {
            // onDie 에 있는 함수를 실행하자.
            if(onDie != null)
            {
                onDie();
            }

            //// Enemy 컴포넌트 가져오자.
            //Enemy enemy = GetComponent<Enemy>();
            //if(enemy != null)
            //{
            //    // 가져온 컴포넌트의 ChangeState 함수를 호출 (Die 상태로 전환)
            //    enemy.ChangeState(Enemy.EEnemyState.DIE);
            //}

            //// PlayerMove 컴포넌트 가져오자.
            //PlayerMove player = GetComponent<PlayerMove>();
            //if(player != null)
            //{
            //    //Die 함수 실행
            //    player.Die();
            //}           

            // 파괴하자.
            //Destroy(gameObject);
        }
    }
}

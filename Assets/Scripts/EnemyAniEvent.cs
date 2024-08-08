using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAniEvent : MonoBehaviour
{
    // 부모의 Enemy 컴포넌트 담을 변수
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // 공격 해야하는 타이밍에 호출되는 함수
    public void OnAttack()
    {
        enemy.RealAttack();
    }
}

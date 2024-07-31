using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    // 최대 HP
    public float maxHP = 3;
    // 현재 HP
    float currHP;

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
            // Enemy 컴포넌트 가져오자.
            // 가져온 컴포넌트의 ChangeState 함수를 호출 (Die 상태로 전환)
            // 파괴하자.
            Destroy(gameObject);
        }
    }
}

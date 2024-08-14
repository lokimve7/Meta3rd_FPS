using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoroutineStudy : MonoBehaviour
{
    // 반환자료형이 void 인 함수를 담을 변수 
    Action<int> action;

    // 반환자료형이 존재하는 함수를 담을 변수
    Func<int,int, string> func;

    void Start()
    {
        // Action 에 직접 함수 넣는 법
        //action = ActionTest;

        // Action 에 람다식으로 넣는 법 (무명함수)
        action = (int num) =>
        {
            print("반환 자료형 void 이고 매개변수 없음!");
        };

        // Action 에 들어가 있는 함수 실행하는 법
        action(1);
        ActionTest(1);

        //Func 에 직접 함수 넣는 법
        func = FuncTest;

        //Func 에 람다식으로 넣는 법
        func = (int a, int b) =>
        {
            return "1111111";
        };

        //Func 에 들어가 있는 함수 실행법
        string result = func(10, 20);
        print(result);


        // 코루틴 함수 실행 방법
        StartCoroutine(Delay(1));
    }

    void Update()
    {
        
    }

    void ActionTest(int num)
    {
        print("반환 자료형 void 이고 매개변수 없음!");
    }

    string FuncTest(int a, int b)
    {
        return "12345678";
    }

    IEnumerator Delay(float time)
    {
        Debug.LogError("코루틴 시작");
        yield return new WaitForSeconds(time);
        Debug.LogError(time + " 이 지났습니다. ");
        yield return new WaitForSeconds(time * 2);
        Debug.LogError("추가로 " + time * 2 + "이 지났습니다.");
    }
}

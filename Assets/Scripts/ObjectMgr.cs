using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ObjectMgr : MonoBehaviour
{
    // Enemy 공장(Prefab)
    public GameObject enemyFactory;

    // 현재 시간
    float currTime;
    // 생성 시간
    float createTime = 1;

    // 실행되는 코루틴 담을 변수
    Coroutine coroutine;
    void Start()
    {
        // 코루틴 시작
        //coroutine = StartCoroutine( Delay() );

        StartCoroutine(StepByStep());
        StartCoroutine(Meta());
    }

    void Update()
    {
        // 코루틴 멈추기
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            // 전체 코루틴 멈추기
            //StopAllCoroutines();
            // 특정 코루틴 멈추기
            StopCoroutine(coroutine);
            coroutine = null;
        }

        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            number++;
        }

        //// 시간을 증가 시키자.
        //currTime += Time.deltaTime;
        //// 만약에 현재시간이 생성시간보다 커지면
        //if(currTime > createTime)
        //{
        //    // Enemy 를 만들자.
        //    CreateEnemy();

        //    // 시간 초기화
        //    currTime = 0;
        //}
    }

    IEnumerator Meta()
    {
        Action<int, string> bbb;
        Func<int, string, float> aaa;
        //yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => { return number == 5; });
        print("number 가 5 되었습니다.");
    }
    int number;
    bool AAA()
    {
        if(number == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator StepByStep()
    {
        print("1초기다린다.");
        yield return new WaitForSeconds(1);
        print("첫번째 공격");

        yield break;
        
        yield return new WaitForSeconds(1);
        print("두번째 공격");

    }

    // 코루틴 함수
    IEnumerator Delay()
    {
        int count = 0;
        while(true)
        {
            print("1초 기다리는 중 ");
            yield return new WaitForSeconds(1);
            print("코루틴 끝");
            CreateEnemy();
            count++;

            if(count > 10)
            {
                yield break;
            }
        }
    }

    void CreateEnemy()
    {
        // 랜덤 위치
        Vector3 pos = new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
        Instantiate(enemyFactory, pos, transform.rotation);      
    }

   
}

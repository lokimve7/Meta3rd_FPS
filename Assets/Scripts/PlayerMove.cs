using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스페이스바를 누르면 점프를 하고 싶다.

public class PlayerMove : MonoBehaviour
{
    // 이동 속력
    public float moveSpeed = 5;
    // Character Controller
    public CharacterController cc;

    // 점프파워
    public float jumpPower = 3;
    // 중력
    float gravity = -9.81f;
    // y 방향 속력
    float yVelocity;

    // 최대 점프 횟수
    public int jumpMaxCnt = 2;
    // 현재 점프 횟수
    int jumpCurrCnt;

    void Start()
    {
        // Character Controller 가져오자
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // W, A, S, D 키로 앞, 왼, 뒤, 오른 으로 움직이게 하자.
        // 1. 사용자의 입력을 받자 (W, A, S, D 키 입력)
        float h = Input.GetAxis("Horizontal");  // A : -1, D : 1, 누르지 않으면 0
        float v = Input.GetAxis("Vertical");    // S : -1, W : 1, 누르지 않으면 0

        // 2. 방향을 정하자.
        Vector3 dirH = transform.right * h;   
        Vector3 dirV = transform.forward * v;
        Vector3 dir =  dirH + dirV;
        // dir 의 크기를 1로 만들자. (벡터의 정규화)
        dir.Normalize();

        // dir 에 speed 곱하자.
        //dir *= moveSpeed;

        // 만약에 땅에 있다면 yVelocity 를 0으로 초기화
        if (cc.isGrounded)
        {
            yVelocity = 0;
            jumpCurrCnt = 0;
        }

        // 만약에 스페이스바를 누르면
        if (Input.GetButtonDown("Jump"))
        //if(Input.GetKeyDown(KeyCode.Space))
        {
            // 만약에 현재 점프 횟수가 최대 점프 횟수 보다 작으면
            if(jumpCurrCnt < jumpMaxCnt)
            {
                // yVelocity 에 jumpPower 를 셋팅
                yVelocity = jumpPower;
                // 현재 점프 횟수를 증가 시키자.
                jumpCurrCnt++;
            }
        }
        // yVelocity 를 중력값을 이용해서 감소시킨다. 
        // v = v0 + at;
        yVelocity += gravity * Time.deltaTime;

       

        // dir.y 값에 yVelocity 를 셋팅
        dir.y = yVelocity;

        // 3. 그 방향으로 움직이자. (P = P0 + vt)
        //transform.position += dir * moveSpeed * Time.deltaTime;
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }    
}

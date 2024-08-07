using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스페이스바를 누르면 점프를 하고 싶다.

public class PlayerMove : MonoBehaviour
{
    // 이동 속력
    public float walkSpeed = 5;
    public float runSpeed = 10;
    float moveSpeed;

    bool isMoving;

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

    // 마우스 우클릭해서 움직일 수 있는 상태니?
    bool canMove = false;
    // 움직여야 하는 방향
    Vector3 moveDir;
    // 움직여야 하는 거리
    float moveDist;

    void Start()
    {
        moveSpeed = walkSpeed;
                
        // Character Controller 가져오자
        cc = GetComponent<CharacterController>();

        // HPSystem 을 가져오자.
        HPSystem hpSystem = GetComponent<HPSystem>();
        // 가져온 컴포넌트에서 Die 함수를 등록
        hpSystem.onDie = Die;
    }

    void Update()
    {
        WASD_Move();

        //Click_Move();

        //if(canMove)
        //{
        //    // 움직일 수 있는 거리를 줄여나가자.
        //    moveDist -= moveSpeed * Time.deltaTime;
        //    // 만약에 moveDist 가 0보다 같거나 작으면
        //    if(moveDist <= 0)
        //    {
        //        // 움직이지 않게 하자.
        //        canMove = false;
        //    }

        //    // dir 방향으로 움직이자.
        //    cc.Move(moveDir * moveSpeed * Time.deltaTime);
        //}
    }  

    void Click_Move()
    {
        // 마우스 오른쪽 버튼을 누르지 않았다면 함수를 나가자.
        if (Input.GetMouseButtonDown(1) == false) return;

        // 화면 좌표에서 Ray를 만들자.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Raycast 를 이용해서 부딪힌 정보를 얻어오자.
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, float.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
        {
            // 움직일 수 있는 상태로 바꾸자.
            canMove = true;
            // Player 이동해야하는 방향을 구하자.(검출 된 위치 - Player 위치)
            moveDir = hitInfo.point - transform.position;
            // 움직여야 하는 거리를 셋팅
            moveDist = moveDir.magnitude;
            // dir 크기를 Normalize 하자.
            moveDir.Normalize();

            //print("클릭 된 물체 이름 : " + hitInfo.transform.name);
            //print("클릭 된 곳의 3D 좌표 : " + hitInfo.point);
        }
    }
    
    void WASD_Move()
    {
        // W, A, S, D 키로 앞, 왼, 뒤, 오른 으로 움직이게 하자.
        // 1. 사용자의 입력을 받자 (W, A, S, D 키 입력)
        float h = Input.GetAxis("Horizontal");  // A : -1, D : 1, 누르지 않으면 0
        float v = Input.GetAxis("Vertical");    // S : -1, W : 1, 누르지 않으면 0

        // 2. 방향을 정하자.
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;

        isMoving = dir.sqrMagnitude > 0;

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
            if (jumpCurrCnt < jumpMaxCnt)
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

    public bool IsMoving()
    {
        return isMoving;
    }

    public void SetMoveSpeed(bool isRun)
    {
        moveSpeed = isRun ? runSpeed : walkSpeed;        
    }

    public GameObject model;
    public void Die()
    {
        // Model 게임오브젝트를 비활성화
        model.SetActive(false);
    }
}

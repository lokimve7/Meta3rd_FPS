﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 총알 파편 효과 공장(Prefab)
    public GameObject bulletImpactFactory;

    // Raycast를 이용한 총알 발사에 검출되는 Layer 설정
    public LayerMask layerMask;

    // 폭탄 공장(Prefab)
    public GameObject bombFactory;

    // 애니메이터
    Animator anim;

    // Aim 모드인지 여부
    bool isAimMode;

    // PlayerMove 컴포넌트
    PlayerMove playerMove;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        // PlayerMove 가져오자.
        playerMove = GetComponent<PlayerMove>();

        // 마우스 잠그자.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 누르면
        if(Input.GetMouseButtonDown(0))
        {
            // 걷는 설정으로 !!
            playerMove.SetWalkRun(false);

            FireRay();
        }

        // 마우스 오른쪽 버튼을 누르면
        if(Input.GetMouseButtonDown(1))
        {
            playerMove.SetWalkRun(false);

            // Aim 모드 (animator "Aim" 파라미터 값을 true)
            isAimMode = true;
            //anim.SetBool("Aim", true);
            
        }
        // 마우스 오른쪽 버튼을 떼면
        if(Input.GetMouseButtonUp(1))
        {
            // Aim 모드 해제 (animator "Aim" 파라미터 값을 false)
            isAimMode = false;
            //anim.SetBool("Aim", false);
        }

        // isAimMode 에 따라서 animator 의 Aiming 값을 변경
        // isAimMode == true 이면 Aiming 값을 0 ---> 1 스르륵 변경
        // isAimMode == false 이면 Aiming 값을 1 ---> 0 스르륵 변경
        anim.SetFloat("Aiming", isAimMode ? 1 : 0, 0.25f * 0.3f, Time.deltaTime);

        // 움직임에 따라서 Idle <-> Walk 로 
        anim.SetFloat("Movement", playerMove.isMoving ? 1 : 0, 0.25f * 0.3f, Time.deltaTime);


        // 2번키를 누르면
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FireBomb();
        }
    }

    void FireBomb()
    {
        // 폭탄 공장(Prefab) 에서 폭탄을 만들자.
        GameObject bomb = Instantiate(bombFactory);
        // 폭탄을 카메라 앞방향으로 1만큼 떨어진 위치 놓자.
        bomb.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1;
        // 폭탄의 앞방향을 카메라의 앞방향으로 설정
        bomb.transform.forward = Camera.main.transform.forward;
    }

    void FireRay()
    {
        // Fire 총 애니 이름 설정
        string fireName = "Fire";
        //// Aim 모드면 총 애니 이름을  Aim_Fire 로
        //if(isAimMode)
        //{
        //    fireName = "Aim_Fire";
        //}
        // 총 쏘는 애니메이션 실행
        anim.CrossFade(fireName, 0.01f, 0, 0);

        // 카메라 위치에서 카메라 앞방향으로 향하는 Ray 를 만들자.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // Ray 를 발사해서 어딘가에 부딪혔다면
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
        {
            // 총알 파편 효과를 생성하자.
            GameObject bulletImpact = Instantiate(bulletImpactFactory);
            // 생성된 효과를 부딪힌 위치에 놓자.
            bulletImpact.transform.position = hitInfo.point;
            // 생성된 효과의 앞방향을 부딪힌 위치의 normal 방향으로 설정

            // 반사각 구하자.
            Vector3 outDirection = Vector3.Reflect(ray.direction, hitInfo.normal);
            bulletImpact.transform.forward = outDirection;

            // 파편효과를 2초뒤에 파괴하자.
            Destroy(bulletImpact, 2);

            // 만약에 총에 맞은 오브젝트가 Enemy 라면
            if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // Enemy 컴포넌트 가져오자.
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                // 가져온 컴포넌트의 OnDamaged 함수를 실행
                enemy.OnDamaged();
            }

            // 부딪힌 오브젝트의 이름과, 부딪힌 위치를 출력해보자.
            //print(hitInfo.transform.name + ", " + hitInfo.point);
            //print(hitInfo.transform.name + ", " + hitInfo.transform.position);

            // 법선 벡터 (면의 수직인 벡터)
            // hitInfo.normal

            // Ray 의 시작 위치에서 부딪힌 위치까지의 거리
            // hitInfo.distance
        }
    }

 }

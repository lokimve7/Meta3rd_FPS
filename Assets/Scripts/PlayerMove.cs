using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    // 이동 속력
    public float moveSpeed = 5;

    void Start()
    {
        
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

        // 3. 그 방향으로 움직이자. (P = P0 + vt)
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}

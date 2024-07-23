using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    // �̵� �ӷ�
    public float moveSpeed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        // W, A, S, D Ű�� ��, ��, ��, ���� ���� �����̰� ����.
        // 1. ������� �Է��� ���� (W, A, S, D Ű �Է�)
        float h = Input.GetAxis("Horizontal");  // A : -1, D : 1, ������ ������ 0
        float v = Input.GetAxis("Vertical");    // S : -1, W : 1, ������ ������ 0

        // 2. ������ ������.
        Vector3 dirH = transform.right * h;   
        Vector3 dirV = transform.forward * v;
        Vector3 dir =  dirH + dirV;
        // dir �� ũ�⸦ 1�� ������. (������ ����ȭ)
        dir.Normalize();        

        // 3. �� �������� ��������. (P = P0 + vt)
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}

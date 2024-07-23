using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    // ȸ�� �ӷ�
    public float rotSpeed = 200;
    // ȸ�� ��
    float rotY;
    float rotX;

    void Start()
    {
        
    }

    void Update()
    {
        // ���콺 �����Ӱ��� �޾ƿ���.
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // ȸ�� ������ ����
        rotY += mx * rotSpeed * Time.deltaTime;
        rotX += my * rotSpeed * Time.deltaTime;

        // rotX �� ���� -80 ~ 80 ���� ���� (�ּҰ�, �ִ밪)
        rotX = Mathf.Clamp(rotX, -80, 80);

        // ��ü�� ȸ�� ������ ���� ����.
        transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
    }
}


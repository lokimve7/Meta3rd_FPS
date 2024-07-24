using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    // 회전 속력
    public float rotSpeed = 200;
    // 회전 값
    float rotX;
    float rotY;

    // 회전 허용
    public bool useRotX;
    public bool useRotY;

    void Start()
    {
        
    }

    void Update()
    {
        // 마우스 움직임값을 받아오자.
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // 회전 각도를 누적
        if(useRotY)   rotY += mx * rotSpeed * Time.deltaTime;        
        if(useRotX)   rotX += my * rotSpeed * Time.deltaTime;

        // rotX 의 값의 -80 ~ 80 도로 제한 (최소값, 최대값)
        rotX = Mathf.Clamp(rotX, -80, 80);

        // 물체를 회전 각도로 셋팅 하자.
        transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
    }

    #region 레퍼런스 타입과 value 타입의 경우 반환되었을 때 멤버변수 접근 허용 / 비허용
    void TestFunc()
    {
        MyTransform myTransform = new MyTransform();
        myTransform.position = new MyVector3(10, 10, 10);
        print(myTransform.position);

        myTransform.SetVector(new MyVector3(10, 10, 10));
        print(myTransform.GetVector().x);
        MyVector3 vec = myTransform.GetVector();
        vec.x = 10;
        //myTransform.position ;
    }
    #endregion
}

#region 레퍼런스 타입과 value 타입의 경우 반환되었을 때 멤버변수 접근 허용 / 비허용
public class MyTransform
{
    public MyVector3 originPosition;

    public MyVector3 position
    {
        get
        {
            return originPosition;
        }
        set
        {
            originPosition = value;
        }
    }

    public MyVector3 GetVector()
    {
        int number = 10;
        return originPosition;
    }
    public void SetVector(MyVector3 value)
    {
        originPosition = value;
    }
}

public struct MyVector3
{
    public MyVector3(int _x, int _y, int _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    public float x;
    public float y;
    public float z;    
}
#endregion



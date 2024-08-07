using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // 나의 앞방향을 카메라의 앞방향 로 설정
        transform.forward = Camera.main.transform.forward;
    }
}

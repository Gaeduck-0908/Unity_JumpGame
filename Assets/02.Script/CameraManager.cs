using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; //따라갈 오브젝트
    public float speed; //카메라 속도

    public Vector2 center; //중앙
    public Vector2 size; //크기

    //적용 
    private void LateUpdate()
    {
        //카메라 위치 이동
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        //속도에 맞춰 따라옴
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; //���� ������Ʈ
    public float speed; //ī�޶� �ӵ�

    public Vector2 center; //�߾�
    public Vector2 size; //ũ��

    //���� 
    private void LateUpdate()
    {
        //ī�޶� ��ġ �̵�
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        //�ӵ��� ���� �����
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}

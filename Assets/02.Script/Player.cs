using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�÷��̾� ����
    public GameObject Idle;
    public GameObject Ready;
    public GameObject Up;

    //�÷��̾� ��������
    private float Player_JumpPower = 20.0f;
    private float Player_JumpTime;
    private bool Player_JumpReady;

    //�÷��̾� �̵��ӵ�
    private float Player_MoveSpeed = 10.0f;

    //�� ����
    private bool Player_IsGround;

    //�⺻ ������Ʈ
    Rigidbody2D rigid;
    SpriteRenderer spr1;
    SpriteRenderer spr2;
    SpriteRenderer spr3;
    AudioSource audio;

    //����
    public PhysicsMaterial2D Bounce, Normal;

    //������Ʈ �ҷ�����
    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spr1 = transform.Find("Player_Idle").GetComponent<SpriteRenderer>();
        spr2 = transform.Find("Player_Ready").GetComponent<SpriteRenderer>();
        spr3 = transform.Find("Player_Up").GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    //������ �Ҵ�
    private void Update()
    {
        PlayerMove();
        PlayerJump();

        if (rigid.velocity.y > 0 && Player_IsGround == false)
        {
            rigid.sharedMaterial = Bounce;
        }
        else
        {
            rigid.sharedMaterial = Normal;
        }
    }

    //������ �κ�
    private void PlayerMove()
    {
        if(Player_IsGround == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            Vector3 move = new Vector3(x, 0, 0) * Player_MoveSpeed * Time.deltaTime;

            //x ���� ���� �¿����
            if (x < 0)
            {
                spr1.flipX = true;
                spr2.flipX = true;
                spr3.flipX = true;
            }
            else if (x > 0)
            {
                spr1.flipX = false;
                spr2.flipX = false;
                spr3.flipX = false;
            }

            transform.position += move;
        }
    }

    //�����κ�
    private void PlayerJump()
    {
        if(Player_IsGround == true)
        {
            if(Input.GetButtonDown("Jump"))
            {
                Player_JumpTime = 0.0f;
                Player_JumpReady = true;
            }
            if(Input.GetButton("Jump"))
            {
                Ready.SetActive(true);
                Idle.SetActive(false);
            }
            if (Input.GetButtonUp("Jump"))
            {
                audio.Play();
                //�������� ���� �������� ����
                if (Player_JumpTime >= 1.0f)
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, Player_JumpPower*2.0f);
                }
                else if (Player_JumpTime >= 0.5f)
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, Player_JumpPower * 1.5f);
                }
                else
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, Player_JumpPower);
                }
              
                Player_IsGround = false;
                Ready.SetActive(false);
                Player_JumpReady = false;
                Up.SetActive(true);
            }
            Player_JumpTime += Time.deltaTime;
        }
    }

    //�浹
    private void OnCollisionEnter2D(Collision2D col)
    {
        switch(col.gameObject.tag)
        {
            //��
            case "Ground":
                Up.SetActive(false);
                Player_IsGround = true;
                Idle.SetActive(true);
                break;
            //�״°�
            case "DeathZone":
                Time.timeScale = 0;
                GameManager.Instance.IsPause = true;
                GameManager.Instance.Restart.SetActive(true);
                break;
            //����
            case "Goal":
                Time.timeScale = 0;
                GameManager.Instance.IsPause = true;
                GameManager.Instance.Clear.SetActive(true);
                break;
        }
    }
}

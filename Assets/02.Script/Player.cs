using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //플레이어 상태
    public GameObject Idle;
    public GameObject Ready;
    public GameObject Up;

    //플레이어 점프관련
    private float Player_JumpPower = 20.0f;
    private float Player_JumpTime;
    private bool Player_JumpReady;

    //플레이어 이동속도
    private float Player_MoveSpeed = 10.0f;

    //땅 여부
    private bool Player_IsGround;

    //기본 컴포넌트
    Rigidbody2D rigid;
    SpriteRenderer spr1;
    SpriteRenderer spr2;
    SpriteRenderer spr3;
    AudioSource audio;

    //물리
    public PhysicsMaterial2D Bounce, Normal;

    //컴포넌트 불러오기
    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spr1 = transform.Find("Player_Idle").GetComponent<SpriteRenderer>();
        spr2 = transform.Find("Player_Ready").GetComponent<SpriteRenderer>();
        spr3 = transform.Find("Player_Up").GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    //움직임 할당
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

    //움직임 부분
    private void PlayerMove()
    {
        if(Player_IsGround == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            Vector3 move = new Vector3(x, 0, 0) * Player_MoveSpeed * Time.deltaTime;

            //x 값에 따라 좌우반전
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

    //점프부분
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
                //프레임을 통해 점프강도 변경
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

    //충돌
    private void OnCollisionEnter2D(Collision2D col)
    {
        switch(col.gameObject.tag)
        {
            //땅
            case "Ground":
                Up.SetActive(false);
                Player_IsGround = true;
                Idle.SetActive(true);
                break;
            //죽는곳
            case "DeathZone":
                Time.timeScale = 0;
                GameManager.Instance.IsPause = true;
                GameManager.Instance.Restart.SetActive(true);
                break;
            //골인
            case "Goal":
                Time.timeScale = 0;
                GameManager.Instance.IsPause = true;
                GameManager.Instance.Clear.SetActive(true);
                break;
        }
    }
}

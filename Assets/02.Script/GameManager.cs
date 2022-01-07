using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lib;

public class GameManager : Singleton<GameManager>
{
    //게임 일시정지 여부
    public bool IsPause;

    //게임 재시작 여부
    public GameObject Restart;

    //게임 클리어 여부
    public GameObject Clear;

    //기본 설정
    private void Start()
    {
        Clear.SetActive(false);
        Restart.SetActive(false);
        Time.timeScale = 1;
        IsPause = false;

        Debug.Log(IsPause);
        Debug.Log(Restart.activeSelf);
        Debug.Log(Clear.activeSelf);
    }

    private void Update()
    {
        if (GameManager.Instance.Restart.activeSelf == true || GameManager.Instance.Clear.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("02.Title");
            }
        }
    }
}

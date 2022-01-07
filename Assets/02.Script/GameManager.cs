using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lib;

public class GameManager : Singleton<GameManager>
{
    //���� �Ͻ����� ����
    public bool IsPause;

    //���� ����� ����
    public GameObject Restart;

    //���� Ŭ���� ����
    public GameObject Clear;

    //�⺻ ����
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

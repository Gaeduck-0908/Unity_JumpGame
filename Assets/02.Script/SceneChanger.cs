using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //씬 컨트롤
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "01.Intro")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("02.Title");
            }
        }
    }

    //게임스타트 버튼
    public void GameStart()
    {
        Soundplay();
        SceneManager.LoadScene("03.Main");
    }

    //게임종료
    public void ExitGame()
    {
        Soundplay();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void Soundplay()
    {
        if (SceneManager.GetActiveScene().name == "02.Title")
        {
            audio.Play();
            DontDestroyOnLoad(audio);
        }
        else
        {

        }
    }
}

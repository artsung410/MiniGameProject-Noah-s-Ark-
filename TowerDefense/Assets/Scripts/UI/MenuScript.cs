using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject _menuUI;

    // Start is called before the first frame update
    void Start()
    {
        _menuUI.SetActive(false);
    }

    public void OpenMenuBtn()
    {
        _menuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeBtn()
    {
        _menuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("MainGameScene");
        Time.timeScale = 1f;

    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}

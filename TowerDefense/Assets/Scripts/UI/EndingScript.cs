using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    [SerializeField] Canvas _peopleCanvas;
    public void RestartBtn()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void PeopleBtn()
    {
        _peopleCanvas.sortingOrder = 2;
    }

    public void ReturnTitleBtn()
    {
        SceneManager.LoadScene("TitleScene");

    }
}

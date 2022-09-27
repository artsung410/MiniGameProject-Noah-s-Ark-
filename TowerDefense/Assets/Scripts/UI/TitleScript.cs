using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    [SerializeField] Canvas _titleCanvas;
    [SerializeField] Canvas _manualCanvas;
    [SerializeField] Canvas _peopleCanvas;

    private void Start()
    {
        _titleCanvas.sortingOrder = 1;
        _manualCanvas.sortingOrder = 0;
        _peopleCanvas.sortingOrder = 0;
    }

    public void GameStartBtn()
    {
        SceneManager.LoadScene("MainGameScene");
    }


    public void ManualBtn()
    {
        _titleCanvas.sortingOrder = 0;
        _manualCanvas.sortingOrder = 1;
        _peopleCanvas.sortingOrder = 0;
    }

    public void PeopleBtn()
    {
        _titleCanvas.sortingOrder = 0;
        _manualCanvas.sortingOrder = 0;
        _peopleCanvas.sortingOrder = 1;
    }

    public void ReturnBtn()
    {
        _titleCanvas.sortingOrder = 1;
        _manualCanvas.sortingOrder = 0;
        _peopleCanvas.sortingOrder = 0;
    }

    
}

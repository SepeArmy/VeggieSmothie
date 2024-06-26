using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanelsManager : MonoBehaviour
{
    public static MainMenuPanelsManager THIS;

    public GameObject[] panels;

    public Button loadButton;

    private void Awake()
    {
        THIS = this;
    }

    public void PanelShow(int index)
    {
        panels[index].SetActive(true);
    }

    public void PanelHide(int index)
    {
        panels[index].SetActive(true);
    }

    public void Actions_Play()
    {
        GameManager.THIS.SetState(GameStates.Transitioning);
    }

    public void Actions_Exit()
    {
        Application.Quit();
    }

    public void Actions_LoadGame()
    {
        GameManager.THIS.SetState(GameStates.Loading);
    }


    public void SetLoadButtonInteractibity(bool _status)
    {
        loadButton.interactable = _status;
    }

}

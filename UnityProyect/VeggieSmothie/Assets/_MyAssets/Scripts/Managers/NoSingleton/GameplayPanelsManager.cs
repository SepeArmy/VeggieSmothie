using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPanelsManager : MonoBehaviour
{
    public static GameplayPanelsManager THIS;

    public GameObject[] panels;

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
}

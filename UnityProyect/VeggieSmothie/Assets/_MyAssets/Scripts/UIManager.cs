using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private Image ramaSlotRefill;
    [SerializeField] private Image enredaderaSlotRefill;
    [SerializeField] private Image setaSlotRefill;
    [SerializeField] private Image despawnSlotRefill;

    private bool gamePaused = false;

    [SerializeField] private CameraManager cam;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            gamePaused = !gamePaused;
            pauseMenu.SetActive(gamePaused);
            if (gamePaused) Time.timeScale = 0.0f;
            else Time.timeScale = 1.0f;
        }
    }

    public void ClickContinue() {
        SoundManager.THIS.PlaySound(Random.Range(4,6));
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void ClickRestart() {
        //SoundManager.THIS.PlaySound(6);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ClickExit() {
        SoundManager.THIS.PlaySound(6);
        Application.Quit();
    }

    public void ClickControls() {
        SoundManager.THIS.PlaySound(Random.Range(4, 6));
        controlsMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void ClickStart() {
        SoundManager.THIS.PlaySound(Random.Range(4, 6));
        cam.OnStart();
        mainMenu.SetActive(false);
    }

    public void ClickCredits() {
        SoundManager.THIS.PlaySound(Random.Range(4, 6));
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ClickBack() {
        SoundManager.THIS.PlaySound(Random.Range(4, 6));
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void RefreshRamaRefill(float value)
    {
        ramaSlotRefill.fillAmount = value;
    }

    public void RefreshEnredaderaRefill(float value)
    {
        enredaderaSlotRefill.fillAmount = value;
    }

    public void RefreshSetaRefill(float value)
    {
        setaSlotRefill.fillAmount = value;
    }

    public void RefreshDespawnRefill(float value) {
        despawnSlotRefill.fillAmount = value;
    }

    public void GameOver() {
        gameOver.SetActive(true);
    }
}

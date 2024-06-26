using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// DESCRIPCION: Gestor de los estados posibles del juego.
/// Coordina
/// 
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager THIS;

    public GameStates state; // estado actual del juego

    public bool gameStarted = false;
    public bool estoyLoco = false;
    bool start;

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
            //DontDestroyOnLoad(gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        estoyLoco = false;
        start = false;
        MusicManager.THIS.MusicPlay(true, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!start && gameStarted)
        {
            MusicManager.THIS.MusicStop();
            MusicManager.THIS.MusicPlay(true, 1);
            start = true;
        }
        
       
           
        
    }

    public void SetState (GameStates newState)
    {
        state = newState;
        Debug.Log("GameManager -> New State: " + state);

        // *****************************************************************
        switch (state)
        {
            // *****************************************************************
            case GameStates.InitApp:

                // Compruebo si existen partidasp previas guardadas...
                // SI -> habilito el boton "Load" del menu de inicio
                // NO -> Hago uso del constructor para iniciar los datos de playerData
                break;
            // *****************************************************************
            case GameStates.MainMenu:
                break;
            // *****************************************************************
            case GameStates.Transitioning:
                break;
            // *****************************************************************
            case GameStates.Playing:
                break;
            // *****************************************************************
            case GameStates.Paused:
                break;
            // *****************************************************************
            case GameStates.LevelCompleted:
                break;
            // *****************************************************************
            case GameStates.GameOver:
                break;
            // *****************************************************************
            case GameStates.Saving:
                break;
            // *****************************************************************
            case GameStates.Loading:
                SetState(GameStates.Transitioning);
                break;
            // *****************************************************************
        }
        // *****************************************************************
    }
}

public enum GameStates
{
    InitApp, // estado predeterminado desde la escena inicial en version exportada
    MainMenu, // menu inicial del juego
    Transitioning, // navegando entre escenas
    Playing,
    Paused,
    LevelCompleted,
    GameOver,
    Saving, // Guardado de datos
    Loading // Carga de datos
}

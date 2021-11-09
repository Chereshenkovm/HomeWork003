using System;
using Core.Sounds;
using UnityEngine;
using GameNamespace;
using Zenject;

[RequireComponent(typeof(SpawnBehaviour))]
[RequireComponent(typeof(ClickMechanics))]
[RequireComponent(typeof(TimeMechanics))]
public class MainMechanics : MonoBehaviour
{
    private SoundManager _soundManager;
    
    [Header("Игровой режим")] 
    [Range(1, 2)] public int GameMode = 1;
    
    [Header("Окна UI")] [SerializeField] private StartWindow _startWindow;
    [SerializeField] private SettingsWindow _settingsWindow;
    [SerializeField] private InstructionWindow _instructionWindow;
    [SerializeField] private ChooseGameWindow _chooseGameWindow;
    [SerializeField] private GameOverScreenWin _gameOverScreen;
    [SerializeField] private SettingsMenuMain _menuMain;
    [SerializeField] private SettingsSound _soundWindow;

    [Header("Время игры")] 
    public int _time = 60;
    
    [Header("Число строк и столбиков")] 
    public int numberOfRows = 2;
    public int numberOfCollumns = 4;

    // Creating object Points to pass through TimeMechanics
    public Points _points = new Points();

    [Header("Файл главной музыки")]
    [SerializeField] private AudioClip music;

    [Inject]
    private void Construct(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }
    
    void Start()
    {
        if (PlayerPrefs.HasKey("isMuted"))
        {
            if (PlayerPrefs.GetString("isMuted") == "Mute")
            {
                AudioListener.volume = 0f;
            }
        }
        
        _startWindow.QuitEvent += () => { Application.Quit(); };

        _startWindow.StartEvent += () =>
        {
            _startWindow.gameObject.SetActive(false);
            _chooseGameWindow.gameObject.SetActive(true);
        };

        _startWindow.SettingsEvent += () =>
        {
            _startWindow.gameObject.SetActive(false);
            _menuMain.gameObject.SetActive(true);
        };

        _startWindow.InstrEvent += () =>
        {
            _startWindow.gameObject.SetActive(false);
            _instructionWindow.gameObject.SetActive(true);
        };

        _settingsWindow.CloseSet += () => { CloseWindow(); };

        _settingsWindow.OnApply += (t, R, C) =>
        {
            _time = Int32.Parse(t);
            numberOfCollumns = C;
            numberOfRows = R;
            CloseWindow();
        };

        _instructionWindow.OnCloseWindow += () =>
        {
            _instructionWindow.gameObject.SetActive(false);
            _startWindow.gameObject.SetActive(true);
        };

        _chooseGameWindow.OnChooseGame1 += () =>
        {
            _chooseGameWindow.gameObject.SetActive(false);
            GameMode = 1;
            StartTheGame();
        };

        _chooseGameWindow.OnChooseGame2 += () =>
        {
            _chooseGameWindow.gameObject.SetActive(false);
            GameMode = 2;
            StartTheGame();
        };

        _chooseGameWindow.OnChooseGame3 += () => { _chooseGameWindow.gameObject.SetActive(false); };

        _gameOverScreen.SetPoints += () => { _gameOverScreen.SetHighScore(_points.points); };

        _gameOverScreen.OnClose += () =>
        {
            _gameOverScreen.SetInteract();
            _gameOverScreen.gameObject.SetActive(false);
            _startWindow.gameObject.SetActive(true);
        };

        _menuMain.OpenSetMenu += () =>
        {
            _settingsWindow.gameObject.SetActive(true);
            _menuMain.gameObject.SetActive(false);
            _settingsWindow.gameObject.SetActive(true);
            _settingsWindow.SetTimeRC(_time.ToString(), numberOfRows, numberOfCollumns);
        };

        _menuMain.CloseSet += () =>
        {
            _menuMain.gameObject.SetActive(false);
            _startWindow.gameObject.SetActive(true);
        };

        _menuMain.OpenSoundSet += () =>
        {
            _soundWindow.gameObject.SetActive(true);
            _menuMain.gameObject.SetActive(false);
            _soundWindow.SetMuteState();
        };

        _soundWindow.CloseSet += () =>
        {
            _soundWindow.gameObject.SetActive(false);
            _menuMain.gameObject.SetActive(true);
        };

        _soundWindow.Mute += () =>
        {
            _soundWindow.SetMute();
        };
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Game Time"))
        {
            _time = PlayerPrefs.GetInt("Game Time");
            numberOfRows = PlayerPrefs.GetInt("NumberOfRows");
            numberOfCollumns = PlayerPrefs.GetInt("NumberOfCols");
        }
        _soundManager.CreateSoundObject().Play(music, transform.position, 0.5f);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Game Time", _time);
        PlayerPrefs.SetInt("NumberOfRows", numberOfRows);
        PlayerPrefs.SetInt("NumberOfCols", numberOfCollumns);
        PlayerPrefs.Save();
    }

    private void CloseWindow()
    {
        _settingsWindow.gameObject.SetActive(false);
        _menuMain.gameObject.SetActive(true);
    }

    public void StartTheGame()
    {
        _points.points = 0;
        Time.timeScale = 1;
        
        if (GameMode == 1)
        {
            gameObject.GetComponent<SpawnBehaviour>().enabled = true;
        }else if (GameMode == 2)
        {
            gameObject.GetComponent<SpawnFlaskBehaviour>().enabled = true;
        }
        
        gameObject.GetComponent<ClickMechanics>().enabled = true;
        gameObject.GetComponent<TimeMechanics>().enabled = true;
    }

    public void StopTheGame()
    {
        if (GameMode == 1)
        {
            _startWindow.SetHighScore(_points.points, 1);
            gameObject.GetComponent<SpawnBehaviour>().enabled = false;
            gameObject.GetComponent<ClickMechanics>().enabled = false;
            gameObject.GetComponent<TimeMechanics>().enabled = false;
        }else if (GameMode == 2)
        {
            _startWindow.SetHighScore(_points.points, 2);
            gameObject.GetComponent<SpawnFlaskBehaviour>().enabled = false;
            gameObject.GetComponent<ClickMechanics>().enabled = false;
            gameObject.GetComponent<TimeMechanics>().enabled = false;
        }
    }

}
using System;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour
{
    [Header("Окно максимально набранных очков в игре Циклопы")]
    [SerializeField] private Text _score;
    [Header("Окно максимально набранных очков в игре Колбы")]
    [SerializeField] private Text _score2;
    
    public event Action StartEvent;
    public event Action QuitEvent;
    public event Action SettingsEvent;
    public event Action InstrEvent;
    
    
    public void OnStart()
    {
        StartEvent?.Invoke();
    }

    public void OnQuit()
    {
        QuitEvent?.Invoke();
    }

    public void OpenSettings()
    {
        SettingsEvent?.Invoke();
    }

    public void OpenInstWindow()
    {
        InstrEvent?.Invoke();
    }
    
    public void SetHighScore(int score, int gameMode)
    {
        if (gameMode == 1)
        {
            if (score > Int32.Parse(_score.text))
                _score.text = score.ToString();
        }else if (gameMode == 2)
        {
            if (score > Int32.Parse(_score2.text))
                _score2.text = score.ToString();
        }
    }
    
    
}

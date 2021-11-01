using System;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour
{
    [SerializeField] private Text _score;
    
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
    
    public void SetHighScore(int score)
    {
        if(score>Int32.Parse(_score.text))
            _score.text = score.ToString();
    }
    
    
}

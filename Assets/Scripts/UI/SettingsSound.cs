using System;
using UnityEngine;

public class SettingsSound : MonoBehaviour
{
    public event Action Mute;
    public event Action CloseSet;

    [Header("Кнопка приглушения звука")]
    [SerializeField] private UnityEngine.UI.Button _muteButton;
    [Header("Текст кнопки приглушения звука")]
    [SerializeField] private UnityEngine.UI.Text _textButton;
    

    public void CloseBack()
    {
        CloseSet?.Invoke();
    }

    public void MuteSound()
    {
        Mute?.Invoke();
    }

    public void SetMute()
    {
        if (_textButton.text == "Mute")
        {
            _textButton.text = "Unmute";
            AudioListener.volume = 0f;
            PlayerPrefs.SetString("isMuted", "Mute");
        }
        else
        {
            _textButton.text = "Mute";
            AudioListener.volume = 1f;
            PlayerPrefs.SetString("isMuted", "Unmute");
        }
    }

    public void SetMuteState()
    {
        if (!PlayerPrefs.HasKey("isMuted"))
        {
            _textButton.text = "Mute";
        }else if (PlayerPrefs.GetString("isMuted") == "Mute")
        {
            _textButton.text = "Unmute";
        }else if (PlayerPrefs.GetString("isMuted") == "Unmute")
        {
            _textButton.text = "Mute";
        }
    }
}

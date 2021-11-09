using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuMain : MonoBehaviour
{

    public event Action OpenSetMenu;
    public event Action OpenSoundSet;
    public event Action CloseSet;
    

    public void CloseBack()
    {
        CloseSet?.Invoke();
    }

    public void SetMenu()
    {
        OpenSetMenu?.Invoke();
    }

    public void SoundMenu()
    {
        OpenSoundSet?.Invoke();
    }
    
    
}

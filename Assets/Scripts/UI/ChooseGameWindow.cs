using System;
using UnityEngine;

public class ChooseGameWindow : MonoBehaviour
{
    public event Action OnChooseGame1;
    public event Action OnChooseGame2;
    public event Action OnChooseGame3;

    public void OpenGame1()
    {
        OnChooseGame1?.Invoke();
    }
    
    public void OpenGame2()
    {
        OnChooseGame2?.Invoke();
    }
    
    public void OpenGame3()
    {
        OnChooseGame3?.Invoke();
    }
    
}

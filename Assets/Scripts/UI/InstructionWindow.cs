using System;
using UnityEngine;

public class InstructionWindow : MonoBehaviour
{
    public event Action OnCloseWindow;

    public void CloseWindow()
    {
        OnCloseWindow?.Invoke();
    }
    
}

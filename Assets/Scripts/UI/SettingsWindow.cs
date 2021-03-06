using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [Header("Поле для ввода времени игры")]
    [SerializeField] private InputField Time;
    [Header("Выбор количества срок для игры в Циклопа")]
    [SerializeField] private Dropdown nRows;
    [Header("Выбор количества столбиков для игры в Циклопа")]
    [SerializeField] private Dropdown nCols;

    public event Action CloseSet;

    public event Action<string, int, int> OnApply;

    public void CloseBack()
    {
        CloseSet?.Invoke();
    }

    public void ApplySetting()
    {
        OnApply?.Invoke(Time.text, Int32.Parse(nRows.options[nRows.value].text), Int32.Parse(nCols.options[nCols.value].text));
    }

    public void SetTimeRC(string _time, int nR, int nC)
    {
        Time.text = _time;
        nRows.value = nRows.options.FindIndex(option => option.text == nR.ToString());
        nCols.value = nCols.options.FindIndex(option => option.text == nC.ToString());
    }
    
}

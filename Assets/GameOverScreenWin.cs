using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenWin : MonoBehaviour
{
    
    [SerializeField] private Text _score;
    [SerializeField] private Button _button;
    [SerializeField] private Text _time;
    public event Action SetPoints;
    public event Action OnClose;
    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        SetPoints?.Invoke();
        _time.text = "5";
        var dt = Int32.Parse(_time.text);
        while (dt > 0)
        {
            yield return new WaitForSeconds(1f);
            dt -= 1;
            _time.text = dt.ToString();
        }

        _button.interactable = true;
    }

    public void CloseWindow()
    {
        OnClose?.Invoke();
    }

    public void SetHighScore(int score)
    {
        _score.text = score.ToString();
    }

    public void SetInteract()
    {
        _button.interactable = false;
    }
    
}

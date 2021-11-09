using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenWin : MonoBehaviour
{
    [Header("Окно отображения набранных очков")]
    [SerializeField] private Text _score;
    [Header("Кнопка возвращения в главное меню")]
    [SerializeField] private Button _button;
    [Header("Таймер")]
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

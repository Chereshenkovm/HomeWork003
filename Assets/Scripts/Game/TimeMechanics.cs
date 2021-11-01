using System;
using System.Collections;
using Game;
using UnityEngine;

public class TimeMechanics : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverMenu;
    
    [Header("Пройденное время с начала игры")]
    public int _time;

    private int startTime;
    private int maxTime = 60;
    private IEnumerator _timer;

    private void OnEnable()
    {
        _time = 0;
        startTime = (int)Time.time;
        
        maxTime = gameObject.GetComponent<MainMechanics>()._time;
        
        _timer = Timer();
        StartCoroutine(_timer);
    }

    private void OnDisable()
    {
        StopCoroutine(_timer);
    }

    IEnumerator Timer()
    {
        while (true)
        {
            _time = (int) (Time.time - startTime);
            if (_time >= maxTime)
            {
                gameObject.GetComponent<MainMechanics>().StopTheGame();
                Debug.Log("Game Over");
                _gameOverMenu.SetActive(true);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}

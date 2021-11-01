using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class UIPointsMechanics : MonoBehaviour
{
    public GameObject main;

    private Text score;
    private Game.Points _mainPoints;

    // Start is called before the first frame update
    void Start()
    {
        _mainPoints = main.GetComponent<MainMechanics>()._points;
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = _mainPoints.points.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;
using UnityEngine.UI;

public class UIPointsMechanics : MonoBehaviour
{
    public GameObject main;

    private Text score;
    private GameNamespace.Points _mainPoints;
    
    void Start()
    {
        _mainPoints = main.GetComponent<MainMechanics>()._points;
        score = GetComponent<Text>();

        StartCoroutine(SetPoints());
    }

    IEnumerator SetPoints()
    {
        while (true)
        {
            score.text = _mainPoints.points.ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeMechanics : MonoBehaviour
{
    private Text _text;
    private TimeMechanics CM;
    private IEnumerator cor;

    public GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        CM = main.GetComponent<TimeMechanics>();
        _text = GetComponent<Text>();
        cor = Timer();
        StartCoroutine(cor);
    }

    IEnumerator Timer()
    {
        while (true)
        {
            _text.text = "Time: " + CM._time.ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(cor);
    }
}

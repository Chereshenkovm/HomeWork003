using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeMechanics : MonoBehaviour
{
    private Text _text;
    private TimeMechanics CM;

    public GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        CM = main.GetComponent<TimeMechanics>();
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "Time: " + CM._time.ToString();
    }
}

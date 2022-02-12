using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Random : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _seed;

    [SerializeField]
    private TMP_InputField _radius;

    private static int _radiusInt;
    public static int Radius => _radiusInt;   

    public void Seed()
    {
        if(string.IsNullOrEmpty(_seed.text)) 
        {
            _seed.text = "0";
        }
        if (string.IsNullOrEmpty(_radius.text))
        {
            _radiusInt = 2;
            _radius.text = "2";
        }
        else _radiusInt = Convert.ToInt32(_radius.text);

        UnityEngine.Random.InitState(Convert.ToInt32(_seed.text));
    }
}

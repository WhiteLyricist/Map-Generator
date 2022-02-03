using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField]
    private GameObject _backlight;

    private bool _active = false;

    private void OnMouseDown()
    {
        _active = !_active;
        _backlight.SetActive(_active); //Активируем, деактивируем "подсветку"
    }
}

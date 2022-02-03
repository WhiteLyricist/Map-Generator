using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveCamera : MonoBehaviour
{
    public static Action EnlargeMap;

    private float _distans;
    private float _maxDistans = 0;

    public void TransformCentr() 
    {
        transform.position = new Vector3(0, 0, -10);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            transform.position = new Vector3(transform.position.x - Input.GetAxis("Mouse X"), transform.position.y - Input.GetAxis("Mouse Y"), transform.position.z); //ѕеремещение камеры

            _distans = Mathf.Sqrt(Mathf.Pow((transform.position.x), 2) + Mathf.Pow((transform.position.y), 2)); //–ассто€ние от начального положени€ камеры (в момент начала игры) до текущего

            if (Mathf.Round(_distans) * 0.5f > _maxDistans)
            {
                _maxDistans = Mathf.Round(_distans) * 0.5f;

                EnlargeMap();

            }
        }
    }
}

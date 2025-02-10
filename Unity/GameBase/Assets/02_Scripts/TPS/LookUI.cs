using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUI : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    private void Update()
    {
        if (_camera != null)
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
             _camera.transform.rotation * Vector3.up);
        }
    }
}

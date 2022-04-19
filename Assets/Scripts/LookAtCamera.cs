using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;

    private void Update()
    {
        _camera ??= Camera.main;

        if (_camera is null)
            return;
        
        transform.LookAt(_camera.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _cameraSpeed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, 
                                         _target.position + _offset,
                                          Time.deltaTime * _cameraSpeed);
    }            
}

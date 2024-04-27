using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    
    void Update()
    {
       transform.position = _cameraTransform.position;
    }
}

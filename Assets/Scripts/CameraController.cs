using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private CameraInputActions _cameraActions;
    private InputAction _rotation;
    
    //Rotation
    [SerializeField] private float _maxRotationSpeed = 1f;


    private void Awake()
    {
        _cameraActions = new CameraInputActions();
    }

    private void OnEnable()
    {
       _cameraActions.Camera.RotateCamera.performed += RotateCamera;
       _cameraActions.Camera.Enable();
    }

    private void OnDisable()
    {
        _cameraActions.Camera.RotateCamera.performed -= RotateCamera;
        _cameraActions.Camera.Disable();
    }
    
    private void RotateCamera(InputAction.CallbackContext inputValue)
    {
        if(!Mouse.current.middleButton.isPressed) return;
        
        var value = inputValue.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f,value * _maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
    }
    
    
}

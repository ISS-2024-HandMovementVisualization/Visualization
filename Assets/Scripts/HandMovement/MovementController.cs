using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace HandMovement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private GameObject _dataProcessorPrefab;
        
        [Header("Fingers")]
        [SerializeField] private GameObject _indexFinger;
        [SerializeField] private GameObject _middleFinger;
        [SerializeField] private GameObject _ringFinger;
        [SerializeField] private GameObject _pinkyFinger;
        [SerializeField] private GameObject _thumb;
        
        private FingerMovement _indexFingerMovement;
        private FingerMovement _middleFingerMovement;
        private FingerMovement _ringFingerMovement;
        private FingerMovement _pinkyFingerMovement;
        private FingerMovement _thumbMovement;
        
        private DataProcessing _dataProcessor;
        
        [SerializeField] private bool _testMode = false;
        
        void Start()
        {   
            _indexFingerMovement = _indexFinger.GetComponent<FingerMovement>();
            _middleFingerMovement = _middleFinger.GetComponent<FingerMovement>();
            _ringFingerMovement = _ringFinger.GetComponent<FingerMovement>();
            _pinkyFingerMovement = _pinkyFinger.GetComponent<FingerMovement>();
            _thumbMovement = _thumb.GetComponent<FingerMovement>();

            _dataProcessor = _dataProcessorPrefab.GetComponent<DataProcessing>();
            _dataProcessor.OnNewDataProcessed.AddListener(HandleAngelsUpdate);
            
            if (_testMode)
            {
                _indexFingerMovement.TestMode = true;
                _middleFingerMovement.TestMode = true;
                _ringFingerMovement.TestMode = true;
                _pinkyFingerMovement.TestMode = true;
                _thumbMovement.TestMode = true;
            }
        }
        
        
        private void HandleAngelsUpdate(float[] fingerAngles)
        {
            var indexFingerBaseAngle = fingerAngles[0];
            var indexFingerMiddleAngle = fingerAngles[1];
            var middleFingerBaseAngle = fingerAngles[2];
            var middleFingerMiddleAngle = fingerAngles[3];
            var ringFingerBaseAngle = fingerAngles[4];
            var ringFingerMiddleAngle = fingerAngles[5];
            var pinkyFingerBaseAngle = fingerAngles[6];
            var pinkyFingerMiddleAngle = fingerAngles[7];
            var thumbBaseAngle = fingerAngles[8];
            var thumbMiddleAngle = fingerAngles[9];
            
            Debug.Log("Data received - MovementController");
            // PIERWSZA WARTOŚĆ TO (TEARZ) ŚRODEK ŚRODKOWEGO PALCA!!!!! PALEC ŚRODKOWY ->> PODŁĄCZONE 
            
            _indexFingerMovement._angleBase = indexFingerBaseAngle;
            _indexFingerMovement._angleMiddle = indexFingerMiddleAngle;

            _middleFingerMovement._angleBase = middleFingerBaseAngle;
            _middleFingerMovement._angleMiddle = middleFingerMiddleAngle;
            
            _ringFingerMovement._angleBase = ringFingerBaseAngle;
            _ringFingerMovement._angleMiddle = ringFingerMiddleAngle;
            
            _pinkyFingerMovement._angleBase = pinkyFingerBaseAngle;
            _pinkyFingerMovement._angleMiddle = pinkyFingerMiddleAngle;
            
            _thumbMovement._angleBase = thumbBaseAngle;
            _thumbMovement._angleMiddle = thumbMiddleAngle;
            
         }

        
        private void OnDestroy()
        {
            _dataProcessor.OnNewDataProcessed.RemoveListener(HandleAngelsUpdate);
        }
    }
}


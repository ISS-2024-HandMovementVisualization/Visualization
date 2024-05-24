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
        
        private FingerMovement _indexFingerMovement;
        private FingerMovement _middleFingerMovement;
        
        private DataProcessing _dataProcessor;
        
        [SerializeField] private bool _testMode = false;
        
        void Start()
        {   
            _indexFingerMovement = _indexFinger.GetComponent<FingerMovement>();
            _middleFingerMovement = _middleFinger.GetComponent<FingerMovement>();

            _dataProcessor = _dataProcessorPrefab.GetComponent<DataProcessing>();

            _dataProcessor.OnNewDataProcessed.AddListener(HandleAngelsUpdate);
            
            if (_testMode)
            {
                _indexFingerMovement.TestMode = true;
                _middleFingerMovement.TestMode = true;
            }
        }
        
        

        private void HandleAngelsUpdate(float indexFingerBaseAngle, float indexFingerMiddleAngle, float middleFingerBaseAngle, float middleFingerMiddleAngle)
        {
            Debug.Log("Data received - MovementController");
            // PIERWSZA WARTOŚĆ TO (TEARZ) ŚRODEK ŚRODKOWEGO PALCA!!!!! PALEC ŚRODKOWY ->> PODŁĄCZONE 

            /*
            _indexFingerMovement._angleBase = indexFingerBaseAngle;
            */
            _indexFingerMovement._angleBase = 0;

            _indexFingerMovement._angleMiddle = indexFingerMiddleAngle;
            /*
            _middleFingerMovement._angleBase = middleFingerBaseAngle;
            */
            _middleFingerMovement._angleBase = 0;

            _middleFingerMovement._angleMiddle = middleFingerMiddleAngle;
         }
        
        private void OnDestroy()
        {
            _dataProcessor.OnNewDataProcessed.RemoveListener(HandleAngelsUpdate);
        }
    }
}


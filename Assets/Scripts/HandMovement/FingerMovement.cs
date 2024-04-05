using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HandMovement
{
    public class FingerMovement : MonoBehaviour
    {
        public bool TestMode = false;
        
        [Header("Finger Parts")]
        [FormerlySerializedAs("finger_middle")] [SerializeField] private GameObject _fingerMiddle;
        [FormerlySerializedAs("finger_tip")] [SerializeField] private GameObject _fingerTip;
        
        [Header("Angles")]
        [Range(-10f, 90f)]
        [FormerlySerializedAs("angle_base")]
        [SerializeField] private float _angleBase;
        
        [FormerlySerializedAs("angle_middle")]
        [Range(-10f, 90f)]
        [SerializeField] private float _angleMiddle;
        
        [FormerlySerializedAs("angle_tip")]
        [Range(-10f, 90f)]   
        [SerializeField] private float _angleTip;
        
        private Transform _fingerMiddleTransform;
        private Transform _fingerTipTransform;
        private Transform _fingerBaseTransform;
        
        private Vector3 _fingerBaseRotation;
        private Vector3 _fingerMiddleRotation;
        private Vector3 _fingerTipRotation;
        
        private readonly float _minAngle = -10f;
        private readonly float _maxAngle = 90f;
        
        void Start()
        {
            _fingerMiddleTransform = _fingerMiddle.transform;
            _fingerTipTransform = _fingerTip.transform;
            _fingerBaseTransform = transform;
            
            _fingerBaseRotation = _fingerBaseTransform.localEulerAngles;
            _fingerMiddleRotation = _fingerMiddleTransform.localEulerAngles;
            _fingerTipRotation = _fingerTipTransform.localEulerAngles;
            
            _angleBase = _fingerBaseTransform.rotation.z;
            _angleMiddle = _fingerMiddleTransform.rotation.z;
            _angleTip = _fingerTipTransform.rotation.z;
        }

        private void Update()
        {
            if (TestMode)
            {
                UpdateFingerAngles(_angleBase, _angleMiddle);
            }
        }

        public void UpdateFingerAngles(float angleBase, float angleMiddle)
        {
            _angleBase = Mathf.Clamp(angleBase, _minAngle, _maxAngle);
            _angleMiddle = Mathf.Clamp(angleMiddle, _minAngle, _maxAngle);
            _angleTip = Mathf.Clamp(angleMiddle*0.8f, _minAngle, _maxAngle);

            _fingerBaseTransform.localEulerAngles = new Vector3(_fingerBaseRotation.x, _fingerBaseRotation.y, -_angleBase);
            _fingerMiddleTransform.localEulerAngles = new Vector3(_fingerMiddleRotation.x, _fingerMiddleRotation.y, -_angleMiddle);
            _fingerTipTransform.localEulerAngles = new Vector3(_fingerTipRotation.x, _fingerTipRotation.y, -_angleTip);
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HandMovement
{
    public class FingerMovement : MonoBehaviour
    {
        [Header("Finger Parts")]
        [FormerlySerializedAs("finger_middle")] [SerializeField] private GameObject _fingerMiddle;
        [FormerlySerializedAs("finger_tip")] [SerializeField] private GameObject _fingerTip;
        
        [Header("Angles")]
        [Range(-3f, 90f)]
        [FormerlySerializedAs("angle_base")]
        [SerializeField] private float _angleBase;
        
        [FormerlySerializedAs("angle_middle")]
        [Range(-3f, 90f)]
        [SerializeField] private float _angleMiddle;
        
        [FormerlySerializedAs("angle_tip")]
        [Range(-3f, 90f)]   
        [SerializeField] private float _angleTip;
        
        private Transform _fingerMiddleTransform;
        private Transform _fingerTipTransform;
        private Transform _fingerBaseTransform;
        
        private readonly float _minAngle = -10f;
        private readonly float _maxAngle = 90f;
        
        void Start()
        {
            _fingerMiddleTransform = _fingerMiddle.transform;
            _fingerTipTransform = _fingerTip.transform;
            _fingerBaseTransform = transform;
            
            _angleBase = _fingerBaseTransform.rotation.z;
            _angleMiddle = _fingerMiddleTransform.rotation.z;
            _angleTip = _fingerTipTransform.rotation.z;
        }

        void Update()
        {
            // update angles for each finger
            _angleBase = Mathf.Clamp(this._angleBase, _minAngle, _maxAngle);
            _angleMiddle = Mathf.Clamp(this._angleMiddle, _minAngle, _maxAngle);
            _angleTip = Mathf.Clamp(this._angleTip, _minAngle, _maxAngle);
            
            _fingerBaseTransform.localEulerAngles = new Vector3(0, 0, -_angleBase);
            _fingerMiddleTransform.localEulerAngles = new Vector3(0, 0, -_angleMiddle);
            _fingerTipTransform.localEulerAngles = new Vector3(0, 0, -_angleTip);
        }
    }
}



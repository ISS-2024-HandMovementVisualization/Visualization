using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandMovement
{
    public class MovementController : MonoBehaviour
    {
        [Header("Fingers")]
        [SerializeField] private GameObject _indexFinger;
        [SerializeField] private GameObject _middleFinger;
        
        private FingerMovement _indexFingerMovement;
        private FingerMovement _middleFingerMovement;
        
        void Start()
        {   
            _indexFingerMovement = _indexFinger.GetComponent<FingerMovement>();
            _middleFingerMovement = _middleFinger.GetComponent<FingerMovement>();
        }

        void Update()
        {
            // update angles for each finger
        }
    }
}


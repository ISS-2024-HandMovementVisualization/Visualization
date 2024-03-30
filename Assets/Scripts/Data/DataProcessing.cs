using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public class DataProcessing : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private GameObject _simpleWebServerPrefab;
        public UnityEvent<float, float, float, float> OnNewDataProcessed;
    
        private SimpleWebServer _simpleWebServer;
        void Start()
        {
            _simpleWebServer = _simpleWebServerPrefab.GetComponent<SimpleWebServer>();
            _simpleWebServer.OnDataGot.AddListener(HandleNewData);
        }
    
        private void HandleNewData(float indexFingerBaseR, float indexFingerMiddleR, float middleFingerBaseR, float middleFingerMiddleR)
        {
            // process data - TO DO
            var indexFingerBaseAngle = indexFingerBaseR * 90;
            var indexFingerMiddleAngle = indexFingerMiddleR * 90;
            var middleFingerBaseAngle = middleFingerBaseR * 90;
            var middleFingerMiddleAngle = middleFingerMiddleR * 90;
            
            // send processed data to MovementController
            OnNewDataProcessed.Invoke(
                indexFingerBaseAngle, 
                indexFingerMiddleAngle, 
                middleFingerBaseAngle,
                middleFingerMiddleAngle);
        }
    
        private void OnDestroy()
        {
            _simpleWebServer.OnDataGot.RemoveListener(HandleNewData);
        }
    }
}

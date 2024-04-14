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
            Debug.Log("Data received - DataProcessing");
            Debug.Log("indexFingerBaseR: " + indexFingerBaseR);
            Debug.Log("indexFingerMiddleR: " + indexFingerMiddleR);
            Debug.Log("middleFingerBaseR: " + middleFingerBaseR);
            Debug.Log("middleFingerMiddleR: " + middleFingerMiddleR);
            // process data - TO DO
            var indexFingerBaseAngle = indexFingerBaseR;
            var indexFingerMiddleAngle = indexFingerMiddleR;
            var middleFingerBaseAngle = middleFingerBaseR;
            var middleFingerMiddleAngle = middleFingerMiddleR;
            
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

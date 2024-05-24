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
        
        [SerializeField] private int _maxValue = 1500;
        [SerializeField] private int _minValue = 800;
    
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
            var indexFingerBaseAngle = indexFingerBaseR / 4000 * 90;
            var indexFingerMiddleAngle = ReadingToAngle(indexFingerMiddleR, 1000, 1200);
            var middleFingerBaseAngle = middleFingerBaseR / 4000 * 90;
            var middleFingerMiddleAngle = ReadingToAngle(middleFingerMiddleR, 700, 900);
            
            // send processed data to MovementController
            OnNewDataProcessed.Invoke(
                indexFingerBaseAngle, 
                indexFingerMiddleAngle, 
                middleFingerBaseAngle,
                middleFingerMiddleAngle);
        }
        private float ReadingToAngle(float reading, float min, float max)
        {
            var normalized = (reading-min) / (max-min);
            return (1 - normalized) * 90;
        }

        private void OnDestroy()
        {
            _simpleWebServer.OnDataGot.RemoveListener(HandleNewData);
        }
    }
}

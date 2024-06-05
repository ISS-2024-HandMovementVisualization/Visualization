using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public class DataProcessing : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private GameObject _simpleWebServerPrefab;
        public UnityEvent<float[]> OnNewDataProcessed;
        
        [SerializeField] private int _maxValue = 1500;
        [SerializeField] private int _minValue = 800;
    
        private SimpleWebServer _simpleWebServer;
        private float[] _fingerAngles;
        void Start()
        {
            _simpleWebServer = _simpleWebServerPrefab.GetComponent<SimpleWebServer>();
            _simpleWebServer.OnDataGot.AddListener(HandleNewData);
        }
    
        private void HandleNewData(float[] fingersR)
        {
            var indexFingerMiddleR = fingersR[0];
            var indexFingerBaseR = fingersR[1];
            var middleFingerMiddleR = fingersR[2];
            var middleFingerBaseR = fingersR[3];            
            var ringFingerMiddleR = fingersR[4];
            var ringFingerBaseR = fingersR[5];
            var pinkyFingerMiddleR = fingersR[6];
            var pinkyFingerBaseR = fingersR[7];
            var thumbMiddleR = fingersR[8];
            var thumbBaseR = fingersR[9];
            
            Debug.Log("Data received - DataProcessing");
            Debug.Log("indexFingerBaseR: " + indexFingerBaseR);
            Debug.Log("indexFingerMiddleR: " + indexFingerMiddleR);
            Debug.Log("middleFingerBaseR: " + middleFingerBaseR);
            Debug.Log("middleFingerMiddleR: " + middleFingerMiddleR);
            
            // process data - TO DO
            /*var indexFingerBaseAngle = ReadingToAngle(indexFingerBaseR , 1000, 1200);
            var indexFingerMiddleAngle = ReadingToAngle(indexFingerMiddleR, 1000, 1200);*/
            var indexFingerBaseAngle = ReadingToAngle(indexFingerBaseR , 630, 800);
            var indexFingerMiddleAngle = ReadingToAngle(indexFingerMiddleR, 5500, 7000);
            var middleFingerBaseAngle = ReadingToAngle(middleFingerBaseR, 1100, 1300);
            var middleFingerMiddleAngle = ReadingToAngle(middleFingerMiddleR, 1200, 1600);
            var ringFingerBaseAngle = ReadingToAngle(ringFingerBaseR, 470, 660);
            var ringFingerMiddleAngle = ReadingToAngle(ringFingerMiddleR, 600, 750);
            var pinkyFingerBaseAngle = ReadingToAngle(pinkyFingerBaseR, 508, 530);
            var pinkyFingerMiddleAngle = ReadingToAngle(pinkyFingerMiddleR, 630, 680);
            var thumbBaseAngle = ReadingToAngle(thumbBaseR, 520, 650);
            var thumbMiddleAngle = ReadingToAngle(thumbMiddleR, 650, 900);
            

            _fingerAngles = new[]
            {
                indexFingerBaseAngle, indexFingerMiddleAngle,
                middleFingerBaseAngle, middleFingerMiddleAngle,
                ringFingerBaseAngle, ringFingerMiddleAngle,
                pinkyFingerBaseAngle, pinkyFingerMiddleAngle,
                thumbBaseAngle, thumbMiddleAngle
            };
            
            // send processed data to MovementController
            OnNewDataProcessed.Invoke(_fingerAngles);
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

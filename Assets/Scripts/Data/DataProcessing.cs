using System.Linq;
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

        private int n = 10;
        
        private float[] _indexFingerBaseAngle_mean = new float[10];
        private float[] _indexFingerMiddleAngle_mean = new float[10];
        private float[] _middleFingerBaseAngle_mean = new float[10];
        private float[] _middleFingerMiddleAngle_mean = new float[10];
        private float[] _ringFingerBaseAngle_mean = new float[10];
        private float[] _ringFingerMiddleAngle_mean = new float[10];
        private float[] _pinkyFingerBaseAngle_mean = new float[10];
        private float[] _pinkyFingerMiddleAngle_mean = new float[10];
        private float[] _thumbBaseAngle_mean = new float[10];
        private float[] _thumbMiddleAngle_mean = new float[10];
        
        private int _counterTo20 = 0;
        private bool _first20Reached = false;
        
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
            var middleFingerMiddleAngle = ReadingToAngle(middleFingerMiddleR, 1400, 1800);
            var ringFingerBaseAngle = ReadingToAngle(ringFingerBaseR, 470, 660);
            var ringFingerMiddleAngle = ReadingToAngle(ringFingerMiddleR, 600, 750);
            var pinkyFingerBaseAngle = ReadingToAngle(pinkyFingerBaseR, 508, 530);
            var pinkyFingerMiddleAngle = ReadingToAngle(pinkyFingerMiddleR, 680, 800);
            var thumbBaseAngle = ReadingToAngle(thumbBaseR, 520, 650);
            var thumbMiddleAngle = ReadingToAngle(thumbMiddleR, 650, 900);
            
            // count mean value for each finger angle - take 20 last values
            

            /*
            if (_counterTo20 < n)
            {
                _indexFingerBaseAngle_mean[_counterTo20] = indexFingerBaseAngle;
                _indexFingerMiddleAngle_mean[_counterTo20] = indexFingerMiddleAngle;
                _middleFingerBaseAngle_mean[_counterTo20] = middleFingerBaseAngle;
                _middleFingerMiddleAngle_mean[_counterTo20] = middleFingerMiddleAngle;
                _ringFingerBaseAngle_mean[_counterTo20] = ringFingerBaseAngle;
                _ringFingerMiddleAngle_mean[_counterTo20] = ringFingerMiddleAngle;
                _pinkyFingerBaseAngle_mean[_counterTo20] = pinkyFingerBaseAngle;
                _pinkyFingerMiddleAngle_mean[_counterTo20] = pinkyFingerMiddleAngle;
                _thumbBaseAngle_mean[_counterTo20] = thumbBaseAngle;
                _thumbMiddleAngle_mean[_counterTo20] = thumbMiddleAngle;
                
                _counterTo20++;
                _first20Reached = true;
            } 
            */


            /*
            if (_first20Reached)
            {
                _counterTo20 %= n;

                indexFingerBaseAngle = _indexFingerBaseAngle_mean.Average();
                indexFingerMiddleAngle = _indexFingerMiddleAngle_mean.Average();
                middleFingerBaseAngle = _middleFingerBaseAngle_mean.Average();
                middleFingerMiddleAngle = _middleFingerMiddleAngle_mean.Average();
                ringFingerBaseAngle = _ringFingerBaseAngle_mean.Average();
                ringFingerMiddleAngle = _ringFingerMiddleAngle_mean.Average();
                pinkyFingerBaseAngle = _pinkyFingerBaseAngle_mean.Average();
                pinkyFingerMiddleAngle = _pinkyFingerMiddleAngle_mean.Average();
                thumbBaseAngle = _thumbBaseAngle_mean.Average();
                thumbMiddleAngle = _thumbMiddleAngle_mean.Average();
            }
            */
            
            

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

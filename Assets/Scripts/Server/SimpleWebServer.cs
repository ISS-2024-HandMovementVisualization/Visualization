using System;
using System.IO;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SimpleWebServer : MonoBehaviour
{
    private HttpListener _listener;
    private bool _isRunning = false;
    
    [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI _text;

    private string _ipAddress = "192.168.253.232";

    // The number of fingers at the moment!!!!
    private int _fingerCount = 2;
    // IMPORTANT!!!
    
    //curl -d "45 34 35 29" -X POST http://192.168.0.145:8080/
    
    public UnityEvent<float, float, float, float> OnDataGot;
    private string _latestValue;


    private void Start()
    {
        StartServer();
    }

    private void OnApplicationQuit()
    {
        StopServer();
    }

    public void StartServer()
    {
        if (!HttpListener.IsSupported)
        {
            Debug.LogError("HttpListener not supported on this platform.");
            return;
        }

        _listener = new HttpListener();
        // Define the URL to listen to (You might need to replace 'localhost' with your machine's IP address)
        _listener.Prefixes.Add($"http://{_ipAddress}:8080/");
        _listener.Start();
        _isRunning = true;
        _listener.BeginGetContext(new AsyncCallback(HandleRequest), _listener);
        Debug.Log($"Server started on http://{_ipAddress}:8080/");
    }

    public void StopServer()
    {
        if (_listener != null)
        {
            _listener.Stop();
            _isRunning = false;
            Debug.Log("Server stopped.");
        }
    }
    
    private void Update() {
        DoHandVisualization(this._latestValue);
    }

    private void DoHandVisualization(string input) {
        this._text.text = input;
    }
    private void HandleRequest(IAsyncResult result)
    {
        if (!_isRunning) return;

        // Obtain the state object and HttpListener context
        HttpListener listener = (HttpListener)result.AsyncState;
        HttpListenerContext context = listener.EndGetContext(result);
        // Listen for the next request
        listener.BeginGetContext(new AsyncCallback(HandleRequest), listener);

        // Initialize an array to hold the floating-point values
        float[] fingerValues = new float[_fingerCount*2];

        // Process request
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        // Check if the request is a POST
        if (request.HttpMethod == "POST") {
            // Read the data sent with the POST request
            using StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string postData = reader.ReadToEnd();
            
            Debug.Log("Post data: "+ postData);
            
            // Parse the postData for the finger values
            string[] values = postData.Split(' ');

            // indexFingerBaseR, indexFingerMiddleR, middleFingerBaseR, middleFingerMiddleR - do ustalenia
            for (int i = 0; i < _fingerCount; i++)
            { 
                fingerValues[i*2] = float.Parse(values[i*2]);
                fingerValues[i*2+1] = float.Parse(values[i*2+1]);
            }

        }
        
        // Respond back with the received values
        string responseString = "";
        for (int i = 0; i < _fingerCount; i++)
        { 
            responseString += $"Finger{i + 1}: {fingerValues[i*2]}, {fingerValues[i*2+1]} <br>";
        }
    
        Debug.Log("Response: " + responseString);
        this._latestValue = responseString;
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);

        response.ContentLength64 = buffer.Length;
        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        
        // Invoke the event with the received values TUUUUUUUUUU PATRZEĆ!!!!! OSTANIE Z PIERWSZYM 
        OnDataGot.Invoke(fingerValues[3], fingerValues[1], fingerValues[2], fingerValues[0]);
        
        /*
        OnDataGot.Invoke(90, 0, 0,0);
        */


        // Close the output stream.
        output.Close(); 
       
    }
}

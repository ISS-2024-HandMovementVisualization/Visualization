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

    // The number of fingers at the moment!!!!
    private int _fingerCount = 2;
    // IMPORTANT!!!
    
    public UnityEvent<float, float, float, float> OnDataGot;


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
        _listener.Prefixes.Add("http://localhost:8080/");
        _listener.Start();
        _isRunning = true;
        _listener.BeginGetContext(new AsyncCallback(HandleRequest), _listener);
        Debug.Log("Server started on http://localhost:8080/");
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

    private string _latestValue;

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
    float[] fingerValues = new float[5];

    // Process request
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;

    // Check if the request is a POST
    if (request.HttpMethod == "POST") {
        // Read the data sent with the POST request
        using StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding);
        string postData = reader.ReadToEnd();
        
        Debug.Log("Post data: "+postData);

        // Parse the postData for the finger values
        string[] pairs = postData.Split('&');
        foreach (string pair in pairs)
        {
            string[] split = pair.Split('=');
            if (split.Length == 2)
            {
                for (int i = 1; i <= _fingerCount; i++)
                {
                    if (split[0] == $"finger{i}" && float.TryParse(split[1], out float value))
                    {
                        fingerValues[i - 1] = value;
                    }
                }
            }
        }
    }

    // Respond back with the received values
    string responseString = "";
    for (int i = 0; i < fingerValues.Length; i++)
    {
        responseString += $"Finger{i + 1}: {fingerValues[i]}<br>";
    }
    this._latestValue = responseString;
    byte[] buffer = Encoding.UTF8.GetBytes(responseString);

    response.ContentLength64 = buffer.Length;
    Stream output = response.OutputStream;
    output.Write(buffer, 0, buffer.Length);
    // Close the output stream.
    output.Close();
}
}

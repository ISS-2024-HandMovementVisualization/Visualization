/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;


public class SerialReader : MonoBehaviour
{
    private string _portName = "COM3";
    private SerialPort serialPort;
    
    [SerializeField] private int _timeout = 100;
    
    private string _receivedData;
    private Thread _readThread;
    
    private bool _readData = true;
    
    void Start()
    {
        serialPort = new SerialPort(_portName, 9600);
        serialPort.ReadTimeout = _timeout;
        serialPort.Open();
        if (serialPort.IsOpen)
        {
            Debug.Log("Port opened");
            _readThread = new Thread(Read);
            _readThread.Start();
        }
        else
        {
            Debug.LogError("Port not opened");
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (serialPort.isOpen)
        {
            ReadData();
        }
    }
    
    private void ReadData()
    {
        try
        {
            _receivedData = serialPort.ReadLine();
            Debug.Log(_receivedData);
        }
        catch (TimeoutException e)
        {
            Debug.LogError(e);
        }
        
    }
    private void OnApplicationQuit()
    {
        _readThread.Abort();
        Debug.Log("Thread aborted");
        serialPort.Close();
    }
}
*/

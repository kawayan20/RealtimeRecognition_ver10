using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;


namespace accelview_classes
{
    class SerialHandler
    {
        public delegate void SerialDataReceivedEventHandler(string message);
        public event SerialDataReceivedEventHandler OnDataReceived;

        public string portName = "";
        public int baudRate = 9600;

        private SerialPort serialPort_;
        private Thread thread_;
        private bool isRunning_ = false;

        private string message_;
        private bool isNewMessageReceived_ = false;
        
    void Awake()
    {
        Open();
    }

    void Update()
    {
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
        }
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort_.Open();

        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();
    }

    private void Close()
    {
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            try
            {
                if (serialPort_.BytesToRead > 0)
                {
                    message_ = serialPort_.ReadLine();
                    isNewMessageReceived_ = true;
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
    public void Write(string message)
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }
    }
}

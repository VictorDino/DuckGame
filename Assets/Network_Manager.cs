using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class Network_Manager : MonoBehaviour
{
    public static Network_Manager _NETWORK_MANAGER;

    private TcpClient socket;
    private NetworkStream stream;

    private bool connected = false;

    private StreamWriter writer;
    private StreamReader reader;

    const string host = "10.40.3.67";
    const int port = 6543;

    private void Awake()
    {
        if (_NETWORK_MANAGER != null && _NETWORK_MANAGER != this)
        {
            Destroy(_NETWORK_MANAGER);
        }
        else
        {
            _NETWORK_MANAGER = this;
            DontDestroyOnLoad(_NETWORK_MANAGER);
        }
    }

    private void Update()
    {
        if (connected)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if(data != null)
                {
                    ManageData(data);
                }
            }
        }
    }

    private void ManageData(string data)
    {
        if (data == "Ping")
        {
            Debug.Log("Recibido ping");
            writer.WriteLine("1");
            writer.Flush();
        }
    }

    public void ConnectToServer(string username, string password)
    {

        try 
        { 
        socket = new TcpClient(host, port);
        stream = socket.GetStream();

        connected = true;

        writer = new StreamWriter(stream);
        reader = new StreamReader(stream);

        writer.WriteLine("0" + "/" + username + "/" + password);
        writer.Flush();
        }
        catch
        {

        }
    }
}

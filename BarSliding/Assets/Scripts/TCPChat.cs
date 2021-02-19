﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;

public class TCPChat : MonoBehaviour {
    #region private members
    /// <summary>
    /// TCPListener to listen for incomming TCP connection
    /// requests.
    /// </summary>
    private TcpListener tcpListener;
    /// <summary>
    /// Background thread for TcpServer workload.
    /// </summary>
    private Thread tcpListenerThread;
    /// <summary>
    /// Create handle to connected tcp client.
    /// </summary>
    private TcpClient connectedTcpClient;
    #endregion

    // Use this for initialization
    void Start () {
        // Start TcpServer background thread
        tcpListenerThread = new Thread (new ThreadStart (ListenForIncommingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start ();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            SendMessage ();
        }
    }

    /// <summary>
    /// Runs in background TcpServerThread; Handles incomming TcpClient requests
    /// </summary>
    ///
    public static string clientMessage = "0";
    private void ListenForIncommingRequests () {
        try {
            // Create listener on localhost port 8052.
            tcpListener = new TcpListener (IPAddress.Parse ("192.168.7.1"), 7000);
            tcpListener.Start ();
            UnityEngine.Debug.Log ("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true) {
                using (connectedTcpClient = tcpListener.AcceptTcpClient ()) {
                    // Get a stream object for reading
                    using (NetworkStream stream = connectedTcpClient.GetStream ()) {
                        int length;
                        // Read incomming stream into byte arrary.
                        while ((length = stream.Read (bytes, 0, bytes.Length)) != 0) {
                            var incommingData = new byte[length];
                            Array.Copy (bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message.

                            clientMessage = Encoding.ASCII.GetString (incommingData);
                           //UnityEngine.Debug.Log("client message received as: " + clientMessage);
                        }
                    }
                }
            }
        } catch (SocketException socketException) {
            UnityEngine.Debug.Log ("SocketException " + socketException.ToString ());
        }
    }
    /// <summary>
    /// Send message to client using socket connection.
    /// </summary>
    private void SendMessage () {
        if (connectedTcpClient == null) {
            return;
        }

        try {
            // Get a stream object for writing.
            NetworkStream stream = connectedTcpClient.GetStream ();
            if (stream.CanWrite) {
                string serverMessage = "This is a message from your server.";

                // Convert string message to byte array.
                byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes (serverMessage);
                // Write byte array to socketConnection stream.
                stream.Write (serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                UnityEngine.Debug.Log ("Server sent his message - should be received by client");
            }
        } catch (SocketException socketException) {
            UnityEngine.Debug.Log ("Socket exception: " + socketException);
        }
    }
}
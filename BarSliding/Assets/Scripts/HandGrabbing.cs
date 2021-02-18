using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; //needs to be UnityEngine.VR in Versions before 2017.2
using System.IO.Ports;
using System;
using System.Net.Sockets;
using System.Net;


public class HandGrabbing : MonoBehaviour
{
    //initialize serial port
    SerialPort stream = new SerialPort("\\\\.\\COM6", 9600);

    float normalizedTime = 0;
    public Animator anim;
    int Vin;
    float fFilt;
    float fac;
    int preset;

GameObject rotater;

    void Start()
    {
        //open serial port
        //stream.Open();

        anim = GetComponent<Animator>();
    }

 
    void Update()
    {   
        if (stream.IsOpen)
        {
            Debug.Log(stream.ReadLine());

        }
        string[] data = TCPChat.clientMessage.Split('.'); //Vin.preset

        Vin = Convert.ToInt32(data[0]);
        preset = Convert.ToInt32(data[1]);

        switch (preset)
        {
            case 1:
                UnityEngine.Debug.Log("Case 1");
                fac = 0.8f;
                fFilt = 4;
                break;
            case 2:
                UnityEngine.Debug.Log("Case 2");
                fac = 0.8f;
                fFilt = 8;
                break;
            case 3:
                UnityEngine.Debug.Log("Case 3");
                fac = 0.8f;
                fFilt = 20;
                break;
            case 4:
                UnityEngine.Debug.Log("Case 4");
                fac = 0.8f;
                fFilt = 4;
                break;
            case 5:
                UnityEngine.Debug.Log("Case 5");
                fac = 0.8f;
                fFilt = 8;
                break;
            case 6:
                UnityEngine.Debug.Log("Case 6");
                fac = 0.8f;
                fFilt = 20;
                break;
            case 7:
                UnityEngine.Debug.Log("Case 7");
                fac = 0.8f;
                fFilt = 4;
                break;
            case 8:
                UnityEngine.Debug.Log("Case 8");
                fac = 0.8f;
                fFilt = 8;
                break;
            case 9:
                UnityEngine.Debug.Log("Case 9");
                fac = 0.8f;
                fFilt = 20;
                break;
            case 10:
                UnityEngine.Debug.Log("Case 10");
                fac = 1;
                fFilt = 4;
                break;
            case 11:
                UnityEngine.Debug.Log("Case 11");
                fac = 1;
                fFilt = 8;
                break;
            case 12:
                UnityEngine.Debug.Log("Case 12");
                fac = 1;
                fFilt = 20;
                break;
            case 13:
                UnityEngine.Debug.Log("Case 13");
                fac = 1;
                fFilt = 4;
                break;
            case 14:
                UnityEngine.Debug.Log("Case 14");
                fac = 1;
                fFilt = 8;
                break;
            case 15:
                UnityEngine.Debug.Log("Case 15");
                fac = 1;
                fFilt = 20;
                break;
            case 16:
                UnityEngine.Debug.Log("Case 16");
                fac = 1;
                fFilt = 4;
                break;
            case 17:
                UnityEngine.Debug.Log("Case 17");
                fac = 1;
                fFilt = 8;
                break;
            case 18:
                UnityEngine.Debug.Log("Case 18");
                fac = 1;
                fFilt = 20;
                break;
            default:
                UnityEngine.Debug.Log("Default case");
             
                break;
        }

        normalizedTime = Vin / 4096f;
        float offset = (20 - fFilt)/20;

        float filteredfFilt = fac*(normalizedTime - offset);
        anim.Play("Press", 0, filteredfFilt);
        anim.speed = 0;

    }



}

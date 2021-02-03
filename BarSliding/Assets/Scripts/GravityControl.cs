﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class GravityControl : MonoBehaviour
{
    SerialPort stream = new SerialPort("\\\\.\\COM12", 9600);
    Rigidbody stick;

    public int gravity=0;
    int Vin1=0;
    int Vin2 =0;
    int velocity = 0;
    Rigidbody rb;
     Rigidbody finger;
     GameObject finger2;
    int fingPos=0;
    public float barDynPos;
    int funcCalled = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.position = new Vector3(-3.06f, 32, 7);
        stream.Open();

        finger = GameObject.Find("hands:b_l_thumb3").GetComponent<Rigidbody>();
        finger2 = GameObject.Find("hands:b_l_index2");

        Physics.gravity = new Vector3(0, 0, 0);



        stick = GetComponent<Rigidbody>();
        stick.position = new Vector3(-3.06f, 32, 7);

    }

    // Update is called once per frame
    void Update()
    {
        if (stream.IsOpen)
        {
            string value = stream.ReadLine();
            //splitting Vin on FSRreading and Joystick reading 

            char[] stringSeparators = new char[] { '+' };

            string[] incoming = value.Split(stringSeparators, System.StringSplitOptions.None);

            Vin1 = int.Parse(incoming[0]);
            Vin2 = int.Parse(incoming[1]);
        }

        





        if (Vin2 == 1000)
        {
            rb.position = new Vector3(-3, 32, 7);
            rb.rotation = Quaternion.Euler(0, 0, 0);
        }
        //Just to monitor 
        barDynPos = this.transform.position.y;
        //-----------Gravity in case of FSR
       // if (barDynPos > -2)
       // {
        //    funcCalled = 0;
        //    gravity = (Vin / 100) - 9;
        //}
        //else if (barDynPos < -2)
        //{
        //    gravity = -9;

            //if (funcCalled != 1 && send.IsOpen)
            //{
            //    send.WriteLine("2");
            //    Debug.Log("Data sent");
            //    funcCalled++;
            //}

        //}
        //falling speed from FSR
        //rb.velocity = new Vector3(0, gravity, 0);
        //rb.rotation = Quaternion.Euler(0, 0, 0);



        //------------ velocity from stick
        velocity = (Vin1 - 496) / 50;
        stick.velocity = new Vector3(0, velocity, 0);


        //hand movement from FSR
        if (Vin2 < 10)
        {
            fingPos = 0;
        }
        else if (Vin2 < 900)
        {
            fingPos = Vin2;

        }
        Debug.Log(Vin2);
        finger.transform.localRotation = Quaternion.Euler(0, 0, -fingPos * 0.005f);
        finger2.transform.localRotation = Quaternion.Euler(0, 0, -47 - fingPos * 0.005f);


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }




}
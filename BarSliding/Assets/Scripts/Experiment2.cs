using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class Experiment2 : MonoBehaviour
{
    Rigidbody stick;
    public string debugVelocity;
    
    SerialPort send = new SerialPort("\\\\.\\COM11", 9600);
    int Vin = 0;
    public int velocity = 0;

    public int velocity2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        send.Open();
        stick = GetComponent<Rigidbody>();
        //stick.position = new Vector3(0, 16, 8);
        stick.position = new Vector3(-3, 32, 7);
        

    }

    // Update is called once per frame
    void Update()
    {
       // Time.timeScale = 0.5f;

        
            string value = send.ReadLine();
            Vin = int.Parse(value);
        
         velocity = (Vin - 496) / 50;
            stick.velocity = new Vector3(0, velocity, 0);
            
        

    }
}

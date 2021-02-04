using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class PositionFromSensor : MonoBehaviour
{
    //SerialPort stream = new SerialPort("\\\\.\\COM12", 9600);

    public Rigidbody stick;
    private KMSSensor value;
    // Start is called before the first frame update
    void Start()
    {
        value = stick.GetComponent<KMSSensor>();
        //stream.Open();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stick.transform.position = new Vector3(-3.06f, 55 + value.pos, 7f);
        //if (stream.IsOpen)
        //{
        //    int data = Mathf.Abs((int)value.pos);
        //    stream.WriteLine(data.ToString());
        //    Debug.Log("data sent: "+ data);
        //}
    }
}

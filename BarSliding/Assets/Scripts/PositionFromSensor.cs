using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class PositionFromSensor : MonoBehaviour
{
    //SerialPort stream = new SerialPort("\\\\.\\COM12", 9600);

    public Rigidbody stick;
    private KMSSensor value;
    private Transform hand;
    private GameObject tempHand;
    public float force;
    public float shiftCoef = 0f;
    public float prevCoef = 0f;



    [Range(5.0f,10f)]
    public float coeficient=5f; 
    // Start is called before the first frame update
    void Start()
    {
        value = stick.GetComponent<KMSSensor>();
        hand = GameObject.Find("RightHand").GetComponent<Transform>();
        tempHand = GameObject.Find("RightHand");
        //stream.Open();

    }

    // Update is called once per frame
    void Update()
    {
        if (coeficient == 0.0f)
        {
            coeficient = 5.0f;
        }
        if (value.pos >= -27)
        {
            force = -2.7f/coeficient;
            //tempHand.active = false;
        }
        else
        {
            force = value.pos / (10f * coeficient);
            //tempHand.active = true;
            

        }

        if (coeficient != prevCoef)
        {
            shiftCoef = force;
            prevCoef = coeficient;
        }
        stick.transform.position = new Vector3(-3.06f, 20 + force-shiftCoef, 7f);
        hand.transform.position = new Vector3(-15.18f, 10.95f + force-shiftCoef, -0.82f); 
        //if (stream.IsOpen)
        //{
        //    int data = Mathf.Abs((int)value.pos);
        //    stream.WriteLine(data.ToString());
        //    Debug.Log("data sent: "+ data);
        //}
    }
}

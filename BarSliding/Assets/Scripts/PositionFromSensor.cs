using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class PositionFromSensor : MonoBehaviour
{

    public Rigidbody stick;
    private KMSSensor value;
    private Transform hand;
    private GameObject tempHand;
    private Transform sphere;

    public float force;
    public float shiftCoef = 0f;
    public float prevCoef = 0f;

    public float fVector;
    Vector3 scaler;


    [Range(5.0f,10f)]
    public float coeficient=5f; 
    // Start is called before the first frame update
    void Start()
    {
        value = stick.GetComponent<KMSSensor>();
        hand = GameObject.Find("RightHand").GetComponent<Transform>();
        tempHand = GameObject.Find("RightHand");

        //sphere = GameObject.Find("Sphere").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (coeficient == 0.0f)
        {
            coeficient = 5.0f;
        }
        if (value.pos >= -30.0f && value.pos<=-24.0f )
        {
            //tempHand.active = false;
            force = value.pos / (10f * coeficient);

        }else if(value.pos <= -30.0f)
        {
            force = -30.0f/ (10f * coeficient);
        }
        else
        {
            force = -2.4f / coeficient;

            //tempHand.active = true;
            

        }

        if (coeficient != prevCoef)
        {
            shiftCoef = force;
            prevCoef = coeficient;
        }
        fVector = force - shiftCoef;
        //scaler = new Vector3(2f - fVector, 2f + fVector, 2f - fVector);
        stick.transform.position = new Vector3(-3.06f, 16.93f + force-shiftCoef, 7f);
        hand.transform.position = new Vector3(-15.18f, 10.95f + force-shiftCoef, -0.82f);

        //sphere.localScale = scaler;

    }
}

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
    public float travelLimit = -35.0f;

    [Range(5.0f,10f)]
    public float coeficient=5f; 
    // Start is called before the first frame update
    void Start()
    {
        value = stick.GetComponent<KMSSensor>();
        hand = GameObject.Find("RightHand").GetComponent<Transform>();
        tempHand = GameObject.Find("RightHand");

        sphere = GameObject.Find("Sphere").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (coeficient == 0.0f)
        {
            coeficient = 5.0f;
        }
        if (value.pos >= travelLimit && value.pos<=-24.0f )
        {
            //tempHand.active = false;
            force = value.pos / (10f * coeficient);

        }else if(value.pos <= travelLimit)
        {
            force = travelLimit / (10f * coeficient);
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
        fVector = (force - shiftCoef)/2f;
        scaler = new Vector3(1f - fVector, 1f + fVector, 1f - fVector);
        stick.transform.position = new Vector3(-3.06f, 14.581f + force-shiftCoef, 7f);
        hand.transform.position = new Vector3(-15.18f, 9.03f + force-shiftCoef, -0.82f);
        sphere.transform.position = new Vector3(-3.055f, 12.073f+fVector, 6.98f);
        sphere.localScale = scaler;

    }
}

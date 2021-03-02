using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapSmusher : MonoBehaviour
{

    public float fVector = 0;
    private Vector3 scaler;
    private Transform sphere;
    private Vector3 scaleMemoriser;
    public float enterValue = 0.0f;
    public bool experiment= true;
    public GameObject hand, stick, lipHand;
    // Start is called before the first frame update
    void Start()
    {
        sphere = GameObject.Find("Sphere").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (experiment)
        {
            hand.SetActive(true);
            stick.SetActive(true);
            lipHand.SetActive(false);
        }
        else
        {
            hand.SetActive(false);
            stick.SetActive(false);
            lipHand.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other){
        enterValue = other.gameObject.transform.position.y;
        //print(fVector);
    }


    void OnTriggerStay(Collider other){
        //print("Still colliding with trigger object " + other.name);
        if (enterValue >= 14.3f)
        {
            fVector = other.gameObject.transform.position.y - enterValue;
        }
        else
        {
            fVector = 0.0f;
        }
        scaler = new Vector3(1f - fVector, 1f + fVector, 1f - fVector);
        //sphere.transform.position = new Vector3(-3.055f, 12.073f + fVector, 6.98f);
        if (fVector >= -0.2f)
        {
            sphere.localScale = scaler;
            scaleMemoriser = scaler;
        }
        else
        {
            sphere.localScale = scaleMemoriser;
            //sphere.localScale = new Vector3(1f, 1f, 1f);
        }


    }
    private void OnTriggerExit(Collider other)
    {
        sphere.localScale = new Vector3(1f, 1f, 1f);
        sphere.transform.position = new Vector3(-3.055f, 12.073f, 6.98f);
    }
}

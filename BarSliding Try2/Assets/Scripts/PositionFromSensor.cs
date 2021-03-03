using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;



public class PositionFromSensor : MonoBehaviour
{

    public GameObject stick;
    private KMSSensor value;
    private Transform hand;
    private GameObject tempHand;
    private Transform sphere;

    public float force;
    public float shiftCoef = 0f;
    public float prevCoef = 0f;

    public float fVector;
    Vector3 scaler;
    //[Range(-45f, -30f)]
    public bool mode0;
    public bool mode30;
    public bool mode40;
    public bool mode45;


    public bool start;
    public bool onceCounter;


    public float travelLimit = -45.0f;
    private Text textSpace;
    public int mode = -2;
    public int pressCounter = 5; 
    public float coeficient=10f;
    public bool entered = true;


    private ModeToKMS kms;
    // Start is called before the first frame update
    void Start()
    {
        value = this.gameObject.GetComponent<KMSSensor>();
        hand = GameObject.Find("RightHand").GetComponent<Transform>();
        tempHand = GameObject.Find("RightHand");
        textSpace = GameObject.Find("Text").GetComponent<Text>();
        sphere = GameObject.Find("Sphere").GetComponent<Transform>();
        kms = this.gameObject.GetComponent<ModeToKMS>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        switch (mode)
        {
            case 0:
                
                mode0 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    
                    StartCoroutine(TimeDealyNextMode(1));

                }
                break;
            case 1:
                mode30 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered ==true)
                {
                    StartCoroutine(TimeDealyNextMode(10));


                }
                break;
            case 10:
                mode30 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(3));


                }
                break;
            case 3:
                mode45 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(8));


                }
                break;
            case 8:
                mode40 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(12));


                }
                break;
            case 12:
                mode45 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(2));


                }
                break;
            case 2:
                mode40 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(7));


                }
                break;
            case 7:
                mode30 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(6));


                }
                break;
            case 6:
                mode45 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(9));


                }
                break;
            case 9:
                mode45 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(11));


                }
                break;
            case 11:
                mode40 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(4));


                }
                break;
            case 4:
                mode30 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(5));


                }
                break;
            case 5:
                mode40 = true;
                textSpace.text = pressCounter.ToString();

                if (value.pos <= travelLimit && pressCounter > 0 && entered)
                {
                    pressCounter--;
                    entered = false;
                }
                else if (value.pos >= -23.5f)
                {
                    entered = true;
                }
                if (pressCounter == 0 && entered == true)
                {
                    StartCoroutine(TimeDealyNextMode(-2));


                }
                break;
            default:
                textSpace.text = "Ready? Say *Yes* if yes";

                break;
                
        }
        if (start && mode!= 0)
        {
            mode = 0;
            start = false;
        }


        if (mode0)
        {
            coeficient = 0.0f;
            mode30 = false;
            mode40 = false;
            mode45 = false;
        }
        if (mode30)
        {
            coeficient = 5.0f;
            travelLimit = -30.0f;
            mode0 = false;
            mode40 = false;
            mode45 = false;
        }
        if (mode40)
        {
            coeficient = 5.0f;
            travelLimit = -40.0f;
            mode0 = false;
            mode30 = false;
            mode45 = false;

        }
        if (mode45)
        {
            coeficient = 5.0f;
            travelLimit = -45.0f;
            mode0 = false;
            mode30 = false;
            mode40 = false;
        }

        if (coeficient == 0.0f)
        {
            coeficient = 10000000f;
        }
        if (value.pos >= travelLimit && value.pos<=-23.5f )
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
        this.gameObject.transform.position = new Vector3(-3.06f, 14.581f + force-shiftCoef, 7f);
        hand.transform.position = new Vector3(-15.18f, 9.03f + force-shiftCoef, -0.82f);

        sphere.transform.position = new Vector3(-3.055f, 12.073f+fVector, 6.98f);
        sphere.localScale = scaler;

    }


    IEnumerator TimeDealyNextMode(int modeNumber)
    {

        //textSpace.text = "Report the numbers 10";
        textSpace.text = "Report the numbers";
        mode0 = true;
        mode30 = false;
        mode40 = false;
        mode45 = false;
        onceCounter = true;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3.0f);
        if (onceCounter)
        {
            mode0 = false;

            pressCounter = 5;
            mode = modeNumber;
            kms.SendMessage(mode.ToString());

            onceCounter = false;
        }


    }
}

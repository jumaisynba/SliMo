using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;



public class PositionFromSensor : MonoBehaviour
{

    private KMSSensor value;
    private Transform hand;
    private Transform sphere;
    private bool send1 = true; 
    public float force;
    private float shiftCoef = 0f;
    private float prevCoef = 0f;

    public float fVector;
    Vector3 scaler;
    //[Range(-45f, -30f)]
    public bool mode0;
    public bool mode30;
    public bool mode40;
    public bool mode45;


    public bool start;
    private bool onceCounter;


    private float travelLimit = -45.0f;
    private Text textSpace;
    public int mode = -2;
    private int pressCounter = 5; 
    private float coeficient=10f;
    private bool entered = true;

    private int tryCount = 1;

    public bool try1b;
    public bool try2b;
    public bool try3b;
    public bool try4b;
    public bool try5b;

    //private LeapSmusher experiment;
    private ModeToKMS kms;
    // Start is called before the first frame update
    void Start()
    {
        value = this.gameObject.GetComponent<KMSSensor>();
        hand = GameObject.Find("RightHand").GetComponent<Transform>();
        textSpace = GameObject.Find("Text").GetComponent<Text>();
        sphere = GameObject.Find("Sphere").GetComponent<Transform>();
        kms = this.gameObject.GetComponent<ModeToKMS>();
        //experiment = GameObject.Find("Sphere").GetComponent<LeapSmusher>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (try1b)
        {
            try1();
        }
        if(try2b)
        {
            try2();
        }
        if (try3b)
        {
            try3();
        }
        if (try4b)
        {
            try4();
        }
        if (try5b)
        {
            try5();
        }



        if (start && mode!= 0)
        {
            mode = 0;
            start = false;
            kms.SendMessage(mode.ToString());
            try1b = true;
            //experiment.experiment = true;
            
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
            coeficient = 10.0f;
            travelLimit = -30.0f;
            mode0 = false;
            mode40 = false;
            mode45 = false;
        }
        if (mode40)
        {
            coeficient = 10.0f;
            travelLimit = -35.0f;
            mode0 = false;
            mode30 = false;
            mode45 = false;

        }
        if (mode45)
        {
            coeficient = 10.0f;
            travelLimit = -40.0f;
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
        
        if (send1)
        {
            kms.SendMessage("0");
            send1 = false;
        }
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3.0f);
        if (onceCounter)
        {
            mode0 = false;

            pressCounter = 5;
            mode = modeNumber;
            kms.SendMessage(mode.ToString());

            onceCounter = false;
            send1 = true;
        }


    }
    public void CountElpases(int modeN)
    {
        if (mode == 0)
        {
            mode0 = true;
        }
        if (mode ==1 || mode == 4 || mode == 7 || mode == 10)
        {
            mode30 = true;
        }
        if (mode == 2 || mode == 5 || mode == 8 || mode == 11)
        {
            mode40 = true;
        }
        if (mode == 3 || mode == 6 || mode == 9 || mode == 12)
        {
            mode45 = true;
        }

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
            
            StartCoroutine(TimeDealyNextMode(modeN));


        }
    }

    public void try1()
    {
        switch (mode)
        {
            case 0:

                CountElpases(1);

                break;
            case 1:
                CountElpases(10);

                break;
            case 10:
                CountElpases(3);

                break;
            case 3:
                CountElpases(8);

                break;
            case 8:
                CountElpases(12);

                break;
            case 12:
                CountElpases(2);

                break;
            case 2:
                CountElpases(7);

                break;
            case 7:
                CountElpases(6);

                break;
            case 6:
                CountElpases(9);
                break;
            case 9:
                CountElpases(11);
                break;
            case 11:
                CountElpases(4);

                break;
            case 4:
                CountElpases(5);

                break;
            case 5:
                CountElpases(-2);

                break;
            default:
                mode = 0;
                try1b = false;
                try2b = true;
                if (tryCount == 1)
                {
                    textSpace.text = "Ready? Say *Yes* if yes";
                }
                break;

        }
    }

    public void try2()
    {

        switch (mode)
        {
            case 0:
                CountElpases(8);

                break;
            case 8:
                CountElpases(4);

                break;
            case 4:
                CountElpases(2);

                break;
            case 2:
                CountElpases(11);

                break;
            case 11:
                CountElpases(12);

                break;
            case 12:
                CountElpases(9);

                break;
            case 9:
                CountElpases(1);

                break;
            case 1:
                CountElpases(3);

                break;
            case 3:
                CountElpases(5);

                break;
            case 5:
                CountElpases(7);

                break;
            case 7:
                CountElpases(6);

                break;
            case 6:
                CountElpases(10);

                break;
            case 10:
                CountElpases(-2);

                break;
            default:
                mode = 0;
                try2b = false;
                try3b = true;
                break;

        }
    }

    public void try3()
    {

        switch (mode)
        {
            case 0:
                CountElpases(5);
                break;
            case 5:
                CountElpases(12);

                break;
            case 12:
                CountElpases(3);

                break;
            case 3:
                CountElpases(9);

                break;
            case 9:
                CountElpases(6);

                break;
            case 6:
                CountElpases(2);

                break;
            case 2:
                CountElpases(7);

                break;
            case 7:
                CountElpases(4);

                break;
            case 4:
                CountElpases(8);

                break;
            case 8:
                CountElpases(1);

                break;
            case 1:
                CountElpases(11);

                break;
            case 11:
                CountElpases(10);

                break;
            case 10:
                CountElpases(-2);

                break;

            default:
                mode = 0;
                try3b = false;
                try4b = true;
                break;

        }
    }

    public void try4()
    {

        switch (mode)
        {
            case 0:
                CountElpases(1);
                break;
            case 1:
                CountElpases(8);

                break;
            case 8:
                CountElpases(9);

                break;
            case 9:
                CountElpases(2);

                break;
            case 2:
                CountElpases(5);

                break;
            case 5:
                CountElpases(11);

                break;
            case 11:
                CountElpases(10);

                break;
            case 10:
                CountElpases(3);

                break;
            case 3:
                CountElpases(6);

                break;
            case 6:
                CountElpases(4);

                break;
            case 4:
                CountElpases(12);

                break;
            case 12:
                CountElpases(7);

                break;
            case 7:
                CountElpases(-2);
                //tryCount = 5;

                break;

            default:
                mode = 0;
                try4b = false;
                try5b = true;
                break;

        }
    }

    public void try5()
    {


        switch (mode)
        {
            case 0:
                CountElpases(2);
                break;
            case 2:
                CountElpases(12);

                break;
            case 12:
                CountElpases(8);

                break;
            case 8:
                CountElpases(7);

                break;
            case 7:
                CountElpases(4);

                break;
            case 4:
                CountElpases(9);

                break;
            case 9:
                CountElpases(10);

                break;
            case 10:
                CountElpases(3);

                break;
            case 3:
                CountElpases(6);

                break;
            case 6:
                CountElpases(5);

                break;
            case 5:
                CountElpases(1);

                break;
            case 1:
                CountElpases(11);

                break;
            case 11:
                CountElpases(-2);
                break;

            default:

                textSpace.text = "THE END. Thank you!";

                break;

        }
    }
}

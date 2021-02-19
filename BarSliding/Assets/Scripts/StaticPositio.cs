using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPositio : MonoBehaviour
{
    Vector3 spherePosition;

    Quaternion rotatiom;

    float x;
    float y;
    float z;
    float xR;
    float yR;
    float zR;
    Vector3 scaler;


    //[Range(0.0f, 1.4f)]
    //public float scaleX;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        xR = transform.rotation.x;
        yR = transform.rotation.y;
        zR = transform.rotation.z;

    }

    // Update is called once per frame
    void Update()
    {
        spherePosition.x = x;
        spherePosition.y = y;//transform.position.y;
        spherePosition.z = z;

        rotatiom.x = xR;
        rotatiom.y = yR;
        rotatiom.z = zR;

        //scaler = new Vector3(2f + scaleX, 2f - scaleX, 2f  + scaleX);

        transform.position = spherePosition;
        transform.rotation = rotatiom;
        //transform.localScale = scaler;
    }
}

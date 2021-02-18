using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPos : MonoBehaviour
{
    Vector3 spherePosition;
    float x;
    float y;
    float z;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        spherePosition.x = x;
        spherePosition.y = transform.position.y;
        spherePosition.z = z;

        transform.position = spherePosition;
        
    }
}

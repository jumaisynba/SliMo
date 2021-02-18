using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPos : MonoBehaviour
{
    Vector3 spherePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spherePosition.x = -2.438f;
        spherePosition.y = 2.18f;
        spherePosition.z = -1.132f;

        transform.position = spherePosition;
    }
}

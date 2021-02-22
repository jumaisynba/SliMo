using UnityEngine;
public class FollowObject : MonoBehaviour
{
    public Transform objectToFollow;
    void Update()
    {
        Vector3 lookOnObject = objectToFollow.position - transform.position;
        lookOnObject = objectToFollow.position - transform.position;
        transform.forward = lookOnObject.normalized;
    }
}
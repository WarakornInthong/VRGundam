using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHand : MonoBehaviour
{
    public Transform followHand;
    public Vector3 rotationOffset;
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        transform.position = followHand.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, followHand.rotation, 0.6f) * Quaternion.Euler(rotationOffset);

        // Debug.Log("owo");
        // Debug.Log(transform.eulerAngles);
        // Debug.Log(followHand.eulerAngles);
    }
}

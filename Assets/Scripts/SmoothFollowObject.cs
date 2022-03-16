using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowObject : MonoBehaviour
{
    public Transform owo;
    
    [Range (0,1)]public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, owo.position,speed);
    }
}

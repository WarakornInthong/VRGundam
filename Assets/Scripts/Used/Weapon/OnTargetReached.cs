using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnTargetReached : MonoBehaviour
{
    public float threshold;
    public Transform target;
    public UnityEvent OnReached;
    public UnityEvent OnRelease;
    private bool wasReached = false;



    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance < threshold && !wasReached){
            OnReached.Invoke();
            // Debug.Log(distance);
            wasReached = true;
        }
        else if(distance >= threshold){
            OnRelease.Invoke();
            wasReached = false;
        }
    }

}

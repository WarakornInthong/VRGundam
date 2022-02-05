using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyRotate : MonoBehaviour
{
    public Transform target;
    [Range(0.5f,5)]
    public float distance;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }

    private void FixedUpdate() {
        float angle = Vector3.Angle(transform.forward, target.forward);
        float owo = transform.forward.magnitude* Mathf.Cos(Mathf.Deg2Rad * angle);
        
        Vector3 newPos = (owo * target.forward).normalized * distance;

        transform.position = target.position + newPos ;
        transform.eulerAngles = target.eulerAngles;

    }
}

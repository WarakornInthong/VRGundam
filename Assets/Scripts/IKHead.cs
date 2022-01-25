using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHead : MonoBehaviour
{
    public Transform rootObject, followObject;
    public Vector3 positionOffset, rotationOffset, headBodyOffset;
    // private Vector3 headBodyOffset;
    // private bool setHead = false;

    void Start()
    {
        // headBodyOffset =  rootObject.position - followObject.position + new Vector3(0, -1.7f, 0);
        // Debug.Log(headBodyOffset);
        
    }

    void LateUpdate() {

            
        headBodyOffset = Vector3.up * -transform.localPosition.y;

        rootObject.position = transform.position + headBodyOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.forward, Vector3.up).normalized;

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    
        // headBodyOffset =  rootObject.position - followObject.position;
    }
}

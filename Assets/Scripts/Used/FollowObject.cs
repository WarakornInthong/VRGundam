using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool smoothMode = false;
    public float followSpeed = 0.03f;
    public bool superAccess;

    void Start() {
        if(superAccess){
            target = GameObject.Find("XR Origin").transform.GetChild(0).GetChild(0);
        }
    }
    void FixedUpdate()
    {
        if(smoothMode){
            Vector3 newPosition = target.position + Vector3.up * offset.y
                            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
                            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;
            Quaternion newRotation = Quaternion.Euler(0, target.eulerAngles.y, 0);

            transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, followSpeed);
        }
        else{
            transform.position = target.position + Vector3.up * offset.y
                                + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
                                + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;
            transform.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
                
        }
    }
}

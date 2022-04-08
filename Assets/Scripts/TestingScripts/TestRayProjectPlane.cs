using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayProjectPlane : MonoBehaviour
{
    public Transform objNormal;
    public Transform origin;
    private void OnDrawGizmos() {
        // Vector3 forwardNormal = Vector3.ProjectOnPlane(transform.position-origin.position, objNormal.forward);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(origin.position, origin.position + origin.forward * 200);
        
        Vector3 rightNormal = Vector3.ProjectOnPlane(transform.position-origin.position, objNormal.right);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin.position, origin.position + rightNormal);

        Vector3 upNormal = Vector3.ProjectOnPlane(transform.position-origin.position, objNormal.forward);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin.position , origin.position + upNormal);
    }
}

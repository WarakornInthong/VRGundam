using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float physicsRange = 0.1f;
    public LayerMask physicsMasks = 0;

    [Range(0, 1)] public float slowDownVelocity = 0.75f;
    [Range(0, 1)] public float slowDownAngularVelocity = 0.85f;

    [Range(0, 100)] public float maxPositionChange = 75.0f;
    [Range(0, 100)] public float maxRotationChange = 75.0f;
    public Transform XROrigin;
    private Rigidbody rb = null;
    public bool isPhysic;

    private Vector3 targetPosition = Vector3.zero;
    public Vector3 tRotation;
    private Quaternion targetRotation = Quaternion.identity;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        targetRotation = Quaternion.Euler(tRotation);
        if(isPhysic)
            RotateUsingPhysics();
        else
            RotateUsingTransform();
    }

    private void RotateUsingTransform(){
        rb.angularVelocity = Vector3.zero;
        transform.localRotation = targetRotation;
    }

    private void RotateUsingPhysics(){
        rb.angularVelocity *= slowDownAngularVelocity;

        Vector3 angularVelocity = FindNewAngularVelocity();

        
        

        if(IsValidValocity(angularVelocity.x)){
            float maxChange = maxRotationChange * Time.deltaTime;
            // rb.angularVelocity = Quaternion.RotateTowards(rb.rotation, targetRotation, maxRotationChange).eulerAngles;
            rb.angularVelocity = Vector3.MoveTowards(rb.angularVelocity, angularVelocity, maxChange);
        }
    }

    private Vector3 FindNewAngularVelocity(){
        // Debug.Log("Rb");
        // Debug.Log(rb.rotation);
        // Debug.Log("Target");
        // Debug.Log(targetRotation);
        // Debug.Log("Origin");
        
        // Debug.Log(XROrigin.rotation);
        Quaternion newT = targetRotation * XROrigin.rotation;
        Quaternion differnce = targetRotation * Quaternion.Inverse(transform.localRotation);
        differnce.ToAngleAxis(out float angularInDegrees, out Vector3 rotationAxis);

        if(angularInDegrees > 180){
            angularInDegrees -= 360;
        }
        
        return (rotationAxis * angularInDegrees * Mathf.Deg2Rad) / Time.deltaTime;
    }

    private bool IsValidValocity(float value){
        
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }
}

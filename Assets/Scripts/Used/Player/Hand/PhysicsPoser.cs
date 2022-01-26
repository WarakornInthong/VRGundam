using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class PhysicsPoser : MonoBehaviour
{
    public float physicsRange = 0.1f;
    public LayerMask physicsMasks = 0;

    [Range(0, 1)] public float slowDownVelocity = 0.75f;
    [Range(0, 1)] public float slowDownAngularVelocity = 0.75f;

    [Range(0, 100)] public float maxPositionChange = 75.0f;
    [Range(0, 100)] public float maxRotationChange = 75.0f;

    private Rigidbody rb = null;
    private XRController controller = null;
    private XRBaseInteractor interactor = null;

    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<XRController>();
        interactor = GetComponent<XRBaseInteractor>();
    }
    
    private void Start(){
        UpdateTracking(controller.inputDevice);
        MoveUsingTransform();
        RotateUsingTransform();
    }

    private void Update(){
        UpdateTracking(controller.inputDevice);
    }

    private void UpdateTracking(InputDevice inputDevice){
        inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out targetPosition);
        inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out targetRotation);
    }

    [System.Obsolete]
    private void FixedUpdate(){
        if(isHoldingObject() || !WithinPhysicsRange()){
            MoveUsingTransform();
            RotateUsingTransform();
        }
        else{
            MoveUsingPhysics();
            RotateUsingPhysics();
        }
    }

    [System.Obsolete]
    private bool isHoldingObject(){
        return interactor.selectTarget;
    }

    public bool WithinPhysicsRange(){
        return Physics.CheckSphere(transform.position, physicsRange, physicsMasks, QueryTriggerInteraction.Ignore);
    }

    private void MoveUsingTransform(){
        rb.velocity = Vector3.zero;
        transform.localPosition = targetPosition;
    }

    private void RotateUsingTransform(){
        rb.angularVelocity = Vector3.zero;
        transform.localRotation = targetRotation;
    }

    private void MoveUsingPhysics(){
        rb.velocity *= slowDownVelocity;

        Vector3 velocity = FindNewVelocity();

        if(IsValidValocity(velocity.x)){
            float maxChange = maxPositionChange * Time.deltaTime;
            rb.velocity = Vector3.MoveTowards(rb.velocity, velocity, maxChange);
        }
    }

    private void RotateUsingPhysics(){
        rb.angularVelocity *= slowDownAngularVelocity;

        Vector3 angularVelocity = FindNewAngularVelocity();

        if(IsValidValocity(angularVelocity.x)){
            float maxChange = maxRotationChange * Time.deltaTime;
            rb.angularVelocity = Vector3.MoveTowards(rb.angularVelocity, angularVelocity, maxChange);
        }
    }
    private Vector3 FindNewVelocity(){
        Vector3 difference = targetPosition - rb.position;
        return difference / Time.deltaTime;
    }

    private Vector3 FindNewAngularVelocity(){
        Quaternion differnce = targetRotation * Quaternion.Inverse(rb.rotation);
        differnce.ToAngleAxis(out float angularInDegrees, out Vector3 rotationAxis);

        if(angularInDegrees > 180){
            angularInDegrees -= 360;
        }

        return (rotationAxis * angularInDegrees * Mathf.Deg2Rad) / Time.deltaTime;
    }

    private bool IsValidValocity(float value){
        
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, physicsRange);
    }

    private void OnValidate() {
        if(TryGetComponent(out Rigidbody rigidbody)){
            rigidbody.useGravity = false;
        }
    }

}

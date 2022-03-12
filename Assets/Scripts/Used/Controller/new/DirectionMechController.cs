using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectionMechController : MonoBehaviour
{
    // Setup
    private Controller controller;
    private Animator animator;

    // Reset Controller Position
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Vector3 defaultForward;
    private Rigidbody rig;
    [SerializeField]
    private ForceMode forceMode;
    public float rSpeed;

    // Target Parameter
    public AimCursor aimCursor;
    public GameObject target;
    public MechGun mechGun;


    void Start()
    {
        // Setup
        controller = GetComponent<Controller>();
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Default position
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        defaultForward = transform.forward;
        
    }

    void FixedUpdate()
    {
        // Controller is used
        if(controller.isUsed){
            
            // Rotate Target
            float direct = transform.position.z -  defaultPosition.z;
            float rotate;
            // Debug.Log(transform.localRotation.eulerAngles.x);
            if(transform.localRotation.eulerAngles.x >= 180){
                rotate = transform.localRotation.eulerAngles.x - 360;
            }
            else{
                rotate = transform.localRotation.eulerAngles.x;
            }
            if(Mathf.Abs(rotate) > 40 && Mathf.Abs(rotate) < 110){
                // Debug.Log(rotate * rSpeed);
                target.transform.Rotate(Vector3.up, Time.deltaTime * -rotate * rSpeed);
            }

            // Aiming
            if(aimCursor != null){
                aimCursor.MapCursor(controller.isUsed);
            }

            // Shooting
            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            device.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger);
            if(trigger){
                // animator.SetBool("Trigger", true);
                mechGun.fire();
            }
            else{
                // animator.SetBool("Trigger", false);
            }

        }
        else{

            // Reset Controller Postion Mechanic
            if(Vector3.Distance(defaultPosition, transform.position) > 0.01f){
                // Force to point
                ReturnToDefaultPosition();
            }
            else{
                // Lock 
                ResetPostion();
            }

        }
    }


    public void ReturnToDefaultPosition(){
        transform.position =  Vector3.Lerp(transform.position, defaultPosition, 0.9f);
        transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, 0.9f);
    }
    public void ResetPostion(){
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        transform.position = defaultPosition;
        transform.forward = defaultForward;

    }
}

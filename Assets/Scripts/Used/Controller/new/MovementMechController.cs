using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMechController : MonoBehaviour
{
    // Setup
    private Controller controller;
    public Transform joyDirection;

    // Reset Controller Position
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Vector3 defaultForward;
    private Rigidbody rig;

    // Target Parameter
    public GameObject target;
    private CharacterController mechCharacter;
    private JaganController jaganController;

    void Start()
    {
        // Setup Controller
        controller = GetComponent<Controller>();
        rig = GetComponent<Rigidbody>();
        
        // Setup Target
        mechCharacter = target.GetComponent<CharacterController>();
        jaganController = target.GetComponent<JaganController>();

        // Default position
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        defaultForward = transform.forward;
        
    }

    void FixedUpdate()
    {
        // Controller is used
        if(controller.isUsed){

            // Control mech movement 
            // mechCharacter.enabled = true;

            // direction that mech move to (dynamics)
            Vector3 jdirect = transform.rotation.eulerAngles;
            if(jdirect.x >= 180){
                jdirect.x -= 360;
            }
            if(jdirect.z >= 180){
                jdirect.z -= 360;
            }
            jdirect.y = 0;



            Quaternion headYaw = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw * -jdirect/10;
            jaganController.moveDirection = direction * Time.fixedDeltaTime * jaganController.moveSpeed;
            // mechCharacter.Move(direction * Time.fixedDeltaTime * jaganController.moveSpeed);

        }
        else{
            jaganController.moveDirection = Vector3.zero;
            // mechCharacter.enabled = false;

            // Reset Controller Postion Mechanic
            if(Vector3.Distance(defaultPosition, transform.position) > 0.01f){
                // Force to point
                ReturnToDefaultPosition();
                // Debug.Log("reset movement");
            }
            else{
                // Lock 
                ResetPostion();
            }
        }
    }

    private void ReturnToDefaultPosition(){
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

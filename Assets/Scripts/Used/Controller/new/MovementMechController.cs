using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMechController : MonoBehaviour
{
    // Setup
    private Controller controller;
    public Transform joyDirection;

    // Target Parameter
    public GameObject target;
    private CharacterController mechCharacter;
    private JaganController jaganController;

    void Start()
    {
        // Setup Controller
        controller = GetComponent<Controller>();
        
        // Setup Target
        mechCharacter = target.GetComponent<CharacterController>();
        jaganController = target.GetComponent<JaganController>();
        
    }

    void FixedUpdate()
    {
        // Controller is used
        if(controller.isUsed){

            // Control mech movement 
            mechCharacter.enabled = true;

            // direction that mech move to (dynamics)
            Vector3 jdirect = Vector3.ProjectOnPlane(joyDirection.up, Vector3.up);
            Quaternion headYaw = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw * new Vector3(-jdirect.z, 0, jdirect.x);
            mechCharacter.Move(direction * Time.fixedDeltaTime * jaganController.moveSpeed);

        }
        else{
            mechCharacter.enabled = false;
        }
    }
}

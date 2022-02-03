using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class Climber : MonoBehaviour
{
    private CharacterController character;

    [HideInInspector]
    public static XRController climbingHand;
    private PlayerMovement playerMovement;
    private float speed = 1;
    private int state = 0;// 0: idle 1: climbing

    void Start()
    {
        character = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        // has climb
        if(climbingHand){
            playerMovement.enabled = false;
            Climb();
            state = 1;
        }
        else{
            // Toggle switch mode
            if(state == 1){
                playerMovement.enabled = true;
                playerMovement.fallingSpeed = 0;
                state = 0;
            }
        }
    }

    private void Climb(){
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime * speed);
    }
}

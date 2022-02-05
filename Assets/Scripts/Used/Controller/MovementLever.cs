using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class MovementLever : Lever
{
    private CharacterController playerCharacter;
    private CharacterController mechCharacter;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<HingeJoint>();
        playerCharacter = player.GetComponent<CharacterController>();
        mechCharacter = target.GetComponent<CharacterController>();
        
        SetVisibleHand(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isUsed){
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
            playerCharacter.enabled = false;
            mechCharacter.enabled = true;
            if(controller.angle >= 5){
                Quaternion headYaw = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
                Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
            
                mechCharacter.Move(direction * Time.fixedDeltaTime * controller.angle/10);
            }
                
            // target.transform.position += new Vector3(inputAxis.x, 0, inputAxis.y) * Time.fixedDeltaTime * controller.angle/10;
        }
        else{
            playerCharacter.enabled = true;
            mechCharacter.enabled = false;
        }
    }
}

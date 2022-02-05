using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class DirectionLever : Lever
{
    private DeviceBasedSnapTurnProvider snapTurn;
    void Start()
    {
        controller = GetComponent<HingeJoint>();
        snapTurn = player.GetComponent<DeviceBasedSnapTurnProvider>();

        SetVisibleHand(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isUsed){
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
            snapTurn.enabled = false;
            Vector3 directionMove = new Vector3(inputAxis.x, inputAxis.y, controller.angle/60);
            // if(directionMove.magnitude > 0.1)
            if(controller.angle >= 5){
                
                if(inputAxis.y < -0.5f || inputAxis.y > 0.5f){
                    // target.transform.GetChild(0).transform.Rotate(target.transform.right, inputAxis.y * 5);
                    target.transform.GetChild(0).transform.eulerAngles +=  new Vector3(inputAxis.y * 1,0,0);
                }
                    
                else
                    target.transform.Rotate(Vector3.up, inputAxis.x * 2);
            }
                
            // Debug.Log(directionMove);
        }
        else{
            snapTurn.enabled = true;
        }
    }

    // public void CheckXRNode(XRNode node){
    //     Debug.Log(node);
    // }
}

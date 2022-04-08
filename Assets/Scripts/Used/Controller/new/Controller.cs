using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Controller : XRGrabInteractable
{
    internal bool isUsed = false;
    
    private void UsedController(bool isGrip){
        isUsed = isGrip;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        UsedController(true);

        base.OnSelectEntering(args);

        if(args.interactorObject is IXRSelectInteractor){
            XRController controller = args.interactorObject.transform.GetComponent<XRController>();
            GameObject player = controller.transform.root.gameObject;
            
            ComponentControl(isUsed, controller.controllerNode, player);
            // if(controller.controllerNode == XRNode.LeftHand && player.name == "XR Origin"){
            //     player.GetComponent<PlayerMovement>().enabled = false;
            //     player.GetComponent<Climber>().enabled = false;
            // }
            // else if(controller.controllerNode == XRNode.RightHand && player.name == "XR Origin"){
            //     player.GetComponent<DeviceBasedSnapTurnProvider>().enabled = false;
            // }
        }
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        UsedController(false);

        base.OnSelectExiting(args);

        if(args.interactorObject is IXRSelectInteractor){
            XRController controller = args.interactorObject.transform.GetComponent<XRController>();
            GameObject player = controller.transform.root.gameObject;
            
            ComponentControl(isUsed, controller.controllerNode, player);

            // if(controller.controllerNode == XRNode.LeftHand && player.name == "XR Origin"){
            //     player.GetComponent<PlayerMovement>().enabled = true;
            //     player.GetComponent<Climber>().enabled = true;
            // }
            // else if(controller.controllerNode == XRNode.RightHand && player.name == "XR Origin"){
            //     player.GetComponent<DeviceBasedSnapTurnProvider>().enabled = true;
            // }
        }
    }


    private void ComponentControl(bool status, XRNode node, GameObject player){
        if(node == XRNode.LeftHand){
            player.GetComponent<PlayerMovement>().enabled = !status;
            player.GetComponent<Climber>().enabled = !status;
        }
        else if(node == XRNode.RightHand){
                player.GetComponent<DeviceBasedSnapTurnProvider>().enabled = !status;
        }
    }

    // [System.Obsolete]
    // protected override void OnSelectEntered(XRBaseInteractor interactor){
    //     base.OnSelectEntered(interactor);

    //     UsedLever(true);
    //     if(interactor is XRDirectInteractor)
    //     {
    //         XRController controller = interactor.GetComponent<XRController>();
    //         GameObject player = controller.gameObject.transform.parent.parent.gameObject;
    //         controller.transform.GetChild(0).GetComponent<Hand>().ToggleVisibility();
    //         if(controller.controllerNode == XRNode.LeftHand){
    //             player.GetComponent<PlayerMovement>().enabled = false;
    //             player.GetComponent<Climber>().enabled = false;
    //         }
    //         else if(controller.controllerNode == XRNode.RightHand){
    //             player.GetComponent<DeviceBasedSnapTurnProvider>().enabled = false;
    //         }
    //         Debug.Log(interactor.GetComponent<XRController>().controllerNode);
    //     }
    // }

    // [System.Obsolete]
    // protected override void OnSelectExited(XRBaseInteractor interactor){
    //     base.OnSelectExited(interactor);

    //     UsedLever(false);
    //     if(interactor is XRDirectInteractor){
    //         XRController controller = interactor.GetComponent<XRController>();
    //         GameObject player = controller.gameObject.transform.parent.parent.gameObject;
    //         controller.transform.GetChild(0).GetComponent<Hand>().ToggleVisibility();
    //         if(controller.controllerNode == XRNode.LeftHand){
    //             player.GetComponent<PlayerMovement>().enabled = true;
    //             player.GetComponent<Climber>().enabled = true;
    //         }
    //         else if(controller.controllerNode == XRNode.RightHand){
    //             player.GetComponent<DeviceBasedSnapTurnProvider>().enabled = true;
    //         }
    //         Debug.Log("Select Exit");
    //     }

    // }
}

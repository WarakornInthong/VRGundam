using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialAttachLocalPosition = Vector3.zero;
    private Quaternion initialAttachLocalRotation = Quaternion.identity;
    public float maxDistance = 0.3f;
    private IXRSelectInteractor interactor = null;
    private bool isGrab = false;
    void Start()
    {
        if(!attachTransform){
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        initialAttachLocalPosition = attachTransform.localPosition;
        initialAttachLocalRotation = attachTransform.localRotation;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args){

        if(args.interactorObject is IXRInteractor){
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
            interactor = args.interactorObject;
            isGrab = true;
        }
        else{
            // interactor = null;
            isGrab = false;
            attachTransform.localPosition = initialAttachLocalPosition;
            attachTransform.localRotation = initialAttachLocalRotation;
        }


        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        isGrab = false;
        base.OnSelectExiting(args);
    }

    void LateUpdate(){
        // test 
        if(isSelected && isGrab){
            // Debug.Log("=w=");
            CheckDistance();
        }
            
    }

    private void CheckDistance(){
        float distance = Vector3.Distance(interactor.transform.position,transform.position);
        if(distance > maxDistance){
            Drop();
            isGrab = false;
            // Debug.Log("Drop");
        }
    }
}

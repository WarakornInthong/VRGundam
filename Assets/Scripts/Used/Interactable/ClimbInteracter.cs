using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteracter : XRBaseInteractable
{
    [SerializeField]public bool isFinal = false;
    protected override void OnSelectEntering(SelectEnterEventArgs args){
        base.OnSelectEntering(args);

        if(args.interactorObject is IXRInteractor){
            Climber.climbingHand = args.interactorObject.transform.GetComponent<XRController>();
            Climber.isFinal = isFinal;
        }
            
    }

    protected override void OnSelectExiting(SelectExitEventArgs args){
        base.OnSelectExiting(args);

        if(args.interactorObject is XRDirectInteractor)
            if(Climber.climbingHand && Climber.climbingHand.name == args.interactorObject.transform.name){
                Climber.climbingHand = null;
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerButtonInput : Singleton<PlayerButtonInput>
{
    [HideInInspector]public bool currentLeftPrimaryButton;
    [HideInInspector]public bool currentLeftSecondaryButton;
    [HideInInspector]public bool currentRightPrimaryButton;
    [HideInInspector]public bool currentRightSecondaryButton;
    private bool[] flags = new bool[4];

    public void OnLeftPrimaryButtonDown(InputAction.CallbackContext value){
        bool input = value.ReadValueAsButton();
        if(input && currentLeftPrimaryButton){
            currentLeftPrimaryButton = false;
        }
        else if(input && !currentLeftPrimaryButton){
            currentLeftPrimaryButton = true;
        }
        else if(!input){
            currentLeftPrimaryButton = false;
        }
    }

    public void OnLeftSecondaryButtonDown(InputAction.CallbackContext value){
        bool input = value.ReadValueAsButton();
        if(input && currentLeftSecondaryButton){
            currentLeftSecondaryButton = false;
        }
        else if(input && !currentLeftSecondaryButton){
            currentLeftSecondaryButton = true;
        }
        else if(!input){
            currentLeftSecondaryButton = false;
        }
    }
    public void OnRightPrimaryButtonDown(InputAction.CallbackContext value){
        currentRightPrimaryButton = value.ReadValueAsButton();
        // Debug.Log( flags[2]);
        // if(flags[2] != currentLeftPrimaryButton){
        //     currentLeftPrimaryButton = flags[2];
        //     Debug.Log(currentRightPrimaryButton);
        // }
    }

    public bool OnClickPrimary(){
        return false;
    }
    public void OnRightSecondaryButtonDown(InputAction.CallbackContext value){
        bool input = value.ReadValueAsButton();
        if(input && currentRightSecondaryButton){
            currentRightSecondaryButton = false;
        }
        else if(input && !currentRightSecondaryButton){
            currentRightSecondaryButton = true;
        }
        else if(!input){
            currentRightSecondaryButton = false;
        }
    }

    void Update()
    {
        // if(flags[2] == true && flags[2] != currentRightPrimaryButton){
        //     currentRightPrimaryButton = true;
        // }
        // else{
        //     currentRightPrimaryButton = false;
        // }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Option : MonoBehaviour
{
    [Header("Player Setting")]    
    public Slider playerHeight;
    public Text playerHeightText;
    private float prevHeight;
    [Header("Movement Type")]
    public Text moveTypeText;
    public bool movementType;
    [Header("Target")]
    public GameObject player;

    void Update()
    {
        ChangeHeight();
        
    }
    private void ChangeHeight(){
        if(prevHeight != playerHeight.value){
            // change character height
            if(player){
                player.GetComponent<XRRig>().cameraYOffset = 1+playerHeight.value;
            }
            // 
            playerHeightText.text = (1+playerHeight.value).ToString();
            prevHeight = playerHeight.value;
        }
    }

    public void Movetype(){
        if(!movementType){
            // Change to Walk
            if(player){
                player.GetComponent<PlayerMovement>().enabled = true;
                player.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                moveTypeText.text = "Walk";
            }
        }
        else{
            // Change to teleport
            if(player){
                player.GetComponent<PlayerMovement>().enabled = false;
                player.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                moveTypeText.text = "Teleport";
            }
        }
        movementType = !movementType;
    }
}

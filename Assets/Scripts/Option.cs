using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
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
    private GameObject player;

    void Awake(){
        player = GameObject.Find("XR Origin");
    }
    void Update()
    {
        ChangeHeight();
        
    }
    private void ChangeHeight(){
        if(prevHeight != playerHeight.value){
            // change character height
            if(player){
                player.GetComponent<XROrigin>().CameraYOffset = 1 + playerHeight.value;
            }
            // 
            playerHeightText.text = (1+playerHeight.value).ToString();
            prevHeight = playerHeight.value;
        }
    }

    public void Movetype(){
        movementType = !movementType;
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
        
    }
}

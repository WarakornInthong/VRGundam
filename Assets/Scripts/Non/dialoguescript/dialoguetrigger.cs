using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class dialoguetrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake(){
        playerInRange = false;
        visualCue.SetActive(false);
        // set ! and bool in false
    }

    private void Update(){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) {


            visualCue.SetActive(true);
            if (Input.GetKey("f")){
                // Debug.Log(inkJSON.text);
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
            
        } else {

            visualCue.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.tag == "Player"){
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if(collider.gameObject.tag == "Player"){
            playerInRange = false;
        }
    }

   
}

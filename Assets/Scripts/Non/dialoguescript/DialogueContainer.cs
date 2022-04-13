using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 size;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset dialogue;
    public bool isTestMode = false;
    void Update(){
        if(CheckPlayerInRange() && !DialogueManager.GetInstance().dialogueIsPlaying) {
            if(isTestMode){
                if (BasicMovementController.currentEnterInput){
                    // Debug.Log(inkJSON.text);
                    DialogueManager.GetInstance().EnterDialogueMode(dialogue);
                }
            }
            
            
        } else {

            // visualCue.SetActive(false);

        }
    }

    public TextAsset GetDialogue(){
        return dialogue;
    }


    public bool CheckPlayerInRange(){
        Collider[] colliders = Physics.OverlapBox(transform.position + offset, size);
        if(colliders.Length > 0){
            foreach(var col in colliders){
                if(col.gameObject.CompareTag("Player")){
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + offset, size);
    }

    // private void OnTriggerEnter(Collider collider) {
    //     if(collider.gameObject.tag == "Player"){
    //         playerInRange = true;
    //     }
    // }

    // private void OnTriggerExit(Collider collider) {
    //     if(collider.gameObject.tag == "Player"){
    //         playerInRange = false;
    //     }
    // }
}

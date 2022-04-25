using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestDialougeContainer : MonoBehaviour
{
    [Header("Dialouge Area")]
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 size;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset dialogue;
    public bool isTestMode = false;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private bool isSetup = false;

    void Start() {
         dialoguePanel.SetActive(false);   
    }

    void Update(){
        if(CheckPlayerInRange() && !TestDialogueManager.Instance.dialogueIsPlaying) {
            if(isTestMode){
                if (BasicMovementController.currentEnterInput){
                    TestDialogueManager.Instance.EnterDialogueMode(dialogue);
                }
            }
            else{
                if(isSetup){
                    TestDialogueManager.Instance.EnterDialogueMode(dialogue);
                }
                else{
                    SetupCurrentDialogue();
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

    private void SetupCurrentDialogue(){
        TestDialogueManager.Instance.InitDialogue(dialoguePanel, dialogueText, choices);
        isSetup = true;
    }
}

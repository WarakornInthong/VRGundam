using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : Singleton<DialogueManager>
{
    //[Header("Dialogue UI")]
    [HideInInspector][SerializeField] private GameObject dialoguePanel;
    [HideInInspector][SerializeField] private TextMeshProUGUI dialogueText;

    //[Header("Choices UI")]

    [HideInInspector][SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }


    private bool flag;
    public void InitDialogue(GameObject i_dialougePanel, TextMeshProUGUI i_dialougeText, GameObject[] i_choices){
        dialoguePanel = i_dialougePanel;
        dialogueText = i_dialougeText;
        choices = i_choices;

        SetupDialogue();
    }

    private void SetupDialogue(){
        dialoguePanel.SetActive(false);
        // create all choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Start(){
        dialogueIsPlaying = false;
    }

    private void Update() {
        if(dialogueIsPlaying){
            if(PlayerButtonInput.Instance.currentRightPrimaryButton == true){
                
                if(flag == false){
                    flag = true;
                    ContinueStroy();
                }
            }
            else if(PlayerButtonInput.Instance.currentRightPrimaryButton == false){
                flag = false;
            }

            if (BasicMovementController.currentLeftMInput){
                ContinueStroy();
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON) {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStroy();
    }

    private IEnumerator ExitDialogueMode() {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStroy(){
        if(currentStory.canContinue){
            // set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            // dispaly choices, if any for this dialogue line
            DisplayChoices();
        }
        else {
            StartCoroutine( ExitDialogueMode());
        }
    }

    private void DisplayChoices() {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support number of choices coming in

        if (currentChoices.Count > choices.Length) {
            Debug.LogError("more choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        //enble and intialize the choices up to the amount of choices for this line dialogue
        foreach(Choice choice in currentChoices) {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index ; i <choices.Length;i++) {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice() {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);

    }

    public void MakeChoice(int choiceIndex){
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStroy();
    }



}

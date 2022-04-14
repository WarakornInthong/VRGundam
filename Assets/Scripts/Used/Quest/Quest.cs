using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    // Properties
    // Default
    [SerializeField]protected string q_name = "No Title";
    [SerializeField]protected string q_description = "No more details";
    [SerializeField]protected bool q_status = false;
    [SerializeField]protected string q_prevQuest;
    [SerializeField]public QuestType q_type;
    // Eliminate
    [SerializeField]private string q_objectiveName;
    [SerializeField]public int q_objectiveNumber;
    [SerializeField]public int countObjective = 0;

    // NPC
    // that give a quest to player
    [SerializeField]protected QuestGiver q_questGiver;
    // that make a quest complete ( can be null, auto complete)
    [SerializeField]protected QuestGiver q_questTarget;

    public Quest(){

    }

    public Quest(QuestType type, string name, string description, string prevQuest, QuestGiver questGiver, QuestGiver questTarget){
        q_type = type;
        q_name = name;
        q_description = description;
        q_status = false;
        q_prevQuest = prevQuest;
        q_questGiver = questGiver;
        q_questTarget = questTarget != null ? questTarget : null;
    }

    public Quest(QuestType type, string name, string description, string prevQuest, QuestGiver questGiver){
        q_type = type;
        q_name = name;
        q_description = description;
        q_status = false;
        q_prevQuest = prevQuest;
        q_questGiver = questGiver;
    }

    // Eliminate Quest
    public Quest(QuestType type, string name, string description, string prevQuest, QuestGiver questGiver,
                          string objectiveName, int objectiveNumber)
    {
        q_type = type;
        q_name = name;
        q_description = description;
        q_status = false;
        q_prevQuest = prevQuest;
        q_questGiver = questGiver;
        q_objectiveName = objectiveName;
        q_objectiveNumber = objectiveNumber;

    }

    public Quest(QuestType type, string name, string description, string prevQuest, QuestGiver questGiver, 
                          QuestGiver questTarget, string objectiveName, int objectiveNumber)
    {
        q_type = type;
        q_name = name;
        q_description = description;
        q_status = false;
        q_prevQuest = prevQuest;
        q_questGiver = questGiver;
        q_questTarget = questTarget != null ? questTarget : null;
        q_objectiveName = objectiveName;
        q_objectiveNumber = objectiveNumber;
    }

    //                                                              Now didn't use
    // // Initial Defalut Quest
    // public void InitQuestDetails(string name, string description, string prevQuest, QuestGiver questGiver, QuestGiver questTarget){
    //     // q_type = type;
    //     q_name = name;
    //     q_description = description;
    //     q_status = false;
    //     q_prevQuest = prevQuest;
    //     q_questGiver = questGiver;
    //     q_questTarget = questTarget != null ? questTarget : null;
    // }

    // // Initial Defalut Eliminate Quest
    // public void InitQuestDetails(string name, string description, string prevQuest, QuestGiver questGiver,
    //                                       QuestGiver questTarget, string objectiveName, int objectiveNumber){
    //     // q_type = type;
    //     q_name = name;
    //     q_description = description;
    //     q_status = false;
    //     q_prevQuest = prevQuest;
    //     q_questGiver = questGiver;
    //     q_questTarget = questTarget != null ? questTarget : null;
    //     q_objectiveName = objectiveName;
    //     q_objectiveNumber = objectiveNumber;
    // }
    // ------------------------------------------------------------------------------------------------------------------------------------------
    public void AbandonQuest(){
        // insert quest to quest giver, and quest from progess list
    }


    // private void CheckProgress(){
    //     // if clear q_status becomes true
    // }

    public void CollectObjective(){
        countObjective++;
        // Debug.Log("Collect!!");
        if(countObjective >= q_objectiveNumber){
            SetComplete();
        }
    }
    public string GetQuestName(){
        return q_name;
    }
    
    public void GetNPCs(out QuestGiver giver, out QuestGiver target){
        giver = q_questGiver;
        target = q_questTarget;
    }

    public QuestGiver GetGiver(){
        return q_questGiver;
    }

    public QuestGiver GetTarget(){
        return q_questTarget;
    }

    public void SetComplete(){
        q_status = true;
        if(q_questTarget == null){
            // Debug.Log("Auto");
            SetAutoCompleteQuest();
        }
    }

    private void SetAutoCompleteQuest(){
        StateManager.ClearQuest(this);
        this.q_questGiver.quests.Remove(this);
    }

}

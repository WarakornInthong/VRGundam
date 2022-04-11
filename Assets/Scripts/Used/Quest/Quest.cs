using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]private string q_name = "No Title";
    [SerializeField]private string q_description = "No more details";
    [SerializeField]protected bool q_status = false;
    [SerializeField]protected string q_prevQuest;
    

    // NPC
    // that give a quest to player
    [SerializeField]protected QuestGiver q_questGiver;
    // that make a quest complete ( can be null, auto complete)
    [SerializeField]protected QuestGiver q_questTarget;

    public Quest(string name, string description, string prevQuest, QuestGiver questGiver, QuestGiver questTarget){
        q_name = name;
        q_description = description;
        q_status = false;
        q_prevQuest = prevQuest;
        q_questGiver = questGiver;
        q_questTarget = questTarget != null ? questTarget : null;
    }

    public Quest(string name, string description, string prevQuest, QuestGiver questGiver){
        q_name = name;
        q_description = description;
        q_status = false;
        q_prevQuest = prevQuest;
        q_questGiver = questGiver;
    }

    public void InitQuestDetails(string name, string description, string prevQuest, QuestGiver questGiver, QuestGiver questTarget){
        q_name = name;
        q_description = description;
        q_status = false;
        q_prevQuest = prevQuest;
        q_questGiver = questGiver;
        q_questTarget = questTarget != null ? questTarget : null;
    }

    public void AbandonQuest(){
        // insert quest to quest giver, and quest from progess list
    }


    private void Update()
    {
        // CheckProgress();
    }

    // private void CheckProgress(){
    //     // if clear q_status becomes true
    // }

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
    }

}

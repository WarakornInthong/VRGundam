using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private static List<Quest> inProgressQuest;
    private static List<Quest> doneQuest;
    public Quest[] p_Quest, d_Quest;
    void Start()
    {
        inProgressQuest = new List<Quest>();
        doneQuest = new List<Quest>();
    }


    void Update()
    {
        if(inProgressQuest.ToArray().Length != p_Quest.Length || doneQuest.ToArray().Length != d_Quest.Length){
            UpdateQuestData();
        }
    }


    // For Debug can delete
    private void UpdateQuestData(){
        p_Quest = inProgressQuest.ToArray();
        d_Quest = doneQuest.ToArray();
        // Debug.Log("Update");
    }

    // Get Quest from QuestGiver
    public static void AddNewQuest(Quest quest){
        if(!inProgressQuest.Contains(quest)){
            if(quest.GetTarget() != null || quest.q_type == QuestType.Eliminate){
                inProgressQuest.Add(quest);
            }
                
            else{
                inProgressQuest.Add(quest);
                quest.SetComplete();
            }
                
            Debug.Log("Get " + quest.GetQuestName());
        }
    }

    // Get Quest from QuestGiver
    public static void ClearQuest(Quest quest){
        if(inProgressQuest.Contains(quest)){
            doneQuest.Add(quest);
            inProgressQuest.Remove(quest);
            Debug.Log("Clear " + quest.GetQuestName());
        }

    }


}

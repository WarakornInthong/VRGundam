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

    // Update is called once per frame
    void Update()
    {
        if(inProgressQuest.ToArray().Length > 0)
            p_Quest = inProgressQuest.ToArray();
        if(doneQuest.ToArray().Length > 0)
            d_Quest = doneQuest.ToArray();
    }

    public static void AddNewQuest(Quest quest){
        if(!inProgressQuest.Contains(quest))
            inProgressQuest.Add(quest);
        Debug.Log("Get " + quest.GetQuestName());
    }

    public static void ClearQuest(Quest quest){
        if(inProgressQuest.Contains(quest)){
            doneQuest.Add(quest);
            inProgressQuest.Remove(quest);
            Debug.Log("Clear " + quest.GetQuestName());
        }

    }

    // private static bool FindQuestInList(Quest quest, in List<Quest> questList){
    //     foreach (var p_quest in questList)
    //     {
    //         if(p_quest.GetQuestName() == quest.GetQuestName()){
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    // void OnDrawGizmos()
    // {
    //     if(inProgressQuest.ToArray().Length > 0){
    //         inProgressQuest[0].GetGiver(out QuestGiver giver, out QuestGiver target);
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawWireSphere(giver.transform.position, 0.5f);

    //         Gizmos.color = Color.blue;
    //         Gizmos.DrawWireSphere(target.transform.position, 0.5f);
    //     }


    // }

}

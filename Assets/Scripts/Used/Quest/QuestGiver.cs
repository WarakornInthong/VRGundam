using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private QusetDetail[] questlist;
    [SerializeField]
    private List<Quest> quests = new List<Quest>();
    private List<Quest> isTargetQuests = new List<Quest>();
    private int questIndex = 0;
    // private bool isGiveQuest = false;
    [SerializeField]
    private bool isTarget = false;
    public UnityEvent ReceiveQuest;
    public UnityEvent SendQuest;
    private bool isAlreadyInArea = false;
    private bool isActivateEvent = false;
    public int size;
    
    void Start()
    {
        InitialCreateQuest();
    }


    void Update()
    {
        if(isTargetQuests.ToArray().Length>0){
            isTarget = true;
        }
        else{
            isTarget = false;
        }

        // ชน
        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one * size);
        // if(colliders.Length > 0 )
        //     Debug.Log(colliders[0].name);
        if(colliders.Length > 0 && colliders[0].gameObject.CompareTag("Player")){
            // 
            isAlreadyInArea = true;
            if(isAlreadyInArea && !isActivateEvent){
                isActivateEvent = true;
                if(isTarget){
                    SendQuest.Invoke();
                }
                else{
                    ReceiveQuest.Invoke();
                }
                

                // if(isGiveQuest){
                //     SendQuest.Invoke();
                //     isGiveQuest = false;
                // }
                // else{
                //     ReceiveQuest.Invoke();
                //     isGiveQuest = true;
                // }
            }
        }
        else{
            isAlreadyInArea = false;
            isActivateEvent = false;
        }
    }

    private void OnDrawGizmos() {
        if(!isTarget){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, Vector3.one * size*2);
        }
        else{
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector3.one * size*2);
        }
        
    }


    public void Test(string text){
        Debug.Log(text);
    }

    private void InitialCreateQuest(){
        if(questlist.Length > 0){
            foreach(QusetDetail q_detail in questlist){
                Quest quest = new Quest(q_detail.name, q_detail.description, "", this, q_detail.targetNPC);
                quests.Add(quest);
            }
        }
    }

    public void GiveQuestToPlayer(){
        
        if(quests.ToArray().Length > 0){
            // Debug.Log(quests[0].GetQuestName());
            StateManager.AddNewQuest(quests[questIndex]);
            QuestGiver target = quests[questIndex].GetTarget();
            target.isTargetQuests.Add(quests[questIndex]);
        }
        
    }

    public void CompleteQuest(){
        if(isTargetQuests.ToArray().Length > 0){        
            Quest dQuest = isTargetQuests[0];
            dQuest.SetComplete();
            StateManager.ClearQuest(dQuest);
            isTargetQuests.Remove(dQuest);
            dQuest.GetGiver().quests.Remove(dQuest);

        }
    }

















    [System.Serializable]
    class QusetDetail{
        public string name; 
        public string description;
        public QuestGiver targetNPC;
        public QuestType type;
    }
    public enum QuestType{

        // only talk to NPC
        Talk = 0,
        // Send item form a npc to another npc
        SendItem = 1,
        // eliminate target enemy
        Eliminate = 2,
        // gather item that requested
        Collect = 3

    }
}

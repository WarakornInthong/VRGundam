using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private QuestDetail[] questlist;
    [SerializeField]
    public List<Quest> quests = new List<Quest>();
    private List<Quest> isTargetQuests = new List<Quest>();
    private int questIndex = 0;
    [SerializeField]
    private bool isTarget = false;
    public UnityEvent ReceiveQuest;
    public UnityEvent SendQuest;

    // check player in area
    private bool isAlreadyInArea = false;
    // check system invoke event
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
        if(colliders.Length > 0){
            // Check all colliders contain Player
            int count = 0;
            foreach(var obj in colliders){
                if(obj.gameObject.CompareTag("Player")){
                    // found player in area
                    isAlreadyInArea = true;
                    count++;
                    // invoke event once time when player in area
                    if(isAlreadyInArea && !isActivateEvent){
                        isActivateEvent = true;
                        if(isTarget){
                            SendQuest.Invoke();
                        }
                        else{
                            if(quests.ToArray().Length > 0)
                                ReceiveQuest.Invoke();
                        }
                    }
                }
            }
            if(count == 0){
                isAlreadyInArea = false;
                isActivateEvent = false;
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

    // Test function can delete
    public void Test(string text){
        Debug.Log(text);
    }

    private void InitialCreateQuest(){
        if(questlist.Length > 0){
            foreach(QuestDetail q_detail in questlist){
                Quest quest;
                if(q_detail.type == QuestType.Eliminate)
                {
                    quest = CreateEliminateQuest(q_detail);
                    // Debug.Log("create eliminate quest");
                    // Debug.Log(q_detail.eliminateTargets.Length);
                    foreach(var target in q_detail.eliminateTargets){
                        // Debug.Log("Send Quest to Target");
                        target.GetComponent<EliminateTarget>().SetQuest(quest);
                    }
                } 
                else
                    quest = CreateTalkQuest(q_detail);
                quests.Add(quest);
            }
        }
    }

    private Quest CreateTalkQuest(QuestDetail detail){
        Quest quest = new Quest(detail.type, detail.name, detail.description, "", this, detail.targetNPC);
        return quest;
    }

    private Quest CreateEliminateQuest(QuestDetail detail){
        Quest quest = new Quest(detail.type, detail.name, detail.description, "", this, detail.targetNPC, detail.objectiveName, detail.eliminateTargets.Length);
        return quest;
    }

    public void GiveQuestToPlayer(){
        
        if(quests.ToArray().Length > 0){
            Quest aQuest = quests[questIndex];
            StateManager.AddNewQuest(aQuest);
            if(aQuest.GetTarget() != null){
                QuestGiver target = aQuest.GetTarget();
                if(!target.isTargetQuests.Contains(aQuest))
                    target.isTargetQuests.Add(aQuest);
            }
        }
        
    }

    // when quest have QuestGiver 
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
    class QuestDetail{
        public string name; 
        public string description;
        public QuestGiver targetNPC;
        public QuestType type;
        public string objectiveName;
        public EliminateTarget[] eliminateTargets;
    }

}

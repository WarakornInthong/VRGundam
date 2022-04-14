using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateTarget : MonoBehaviour
{
    [SerializeField]
    private Quest t_quest;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuest(Quest quest){
        t_quest = quest;
        // Debug.Log("Set to be target");
         
    }

    // For Test Only use in UnityEvent
    public void TestDead(){
        float time = Random.Range(1f, 4f);
        StartCoroutine(CountDown(time));
    }
    public void Dead(){
        if(t_quest != null){
            t_quest.CollectObjective();
        }
    }

    // For Test Only
    IEnumerator CountDown(float time){
        yield return new WaitForSeconds(time);
        // Dead();
        // Debug.Log(gameObject.name + " Dead");

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnDestroy() {
        // Debug.Log(t_quest.countObjective + "/" + t_quest.q_objectiveNumber);
        Dead();
    }
}

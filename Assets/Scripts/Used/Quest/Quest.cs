using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]private string q_name = "No Title";
    [SerializeField]private string q_description = "No more details";
    [SerializeField]protected bool q_status = false;
    [SerializeField]protected string q_prevQuest;

    public void InitQuestDetails(string name, string description, string prevQuest){
        q_name = name;
        q_description = description;
        q_status = false;
        q_prevQuest = prevQuest;
    }

    private void Update()
    {
        CheckProgress();
    }

    private void CheckProgress(){
        // if clear q_status becomes true
    }
}

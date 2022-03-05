using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private QusetDetail[] questlist;
    void Start()
    {
        
    }


    void Update()
    {
        
    }


    [System.Serializable]
    class QusetDetail{
        public string name; 
        public string description;
        public string type;
    }
}

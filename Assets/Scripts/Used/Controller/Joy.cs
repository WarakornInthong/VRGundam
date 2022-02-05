using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joy : MonoBehaviour
{
    public Transform joyDirection;
    internal bool isUsed = false;
    public GameObject player;
    public GameObject target;
    
    public void UsedLever(bool isGrip){
        isUsed = isGrip;
    }
}

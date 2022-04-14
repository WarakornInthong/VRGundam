using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum QuestType
{
    // only talk to NPC
    Talk = 0,
    // Send item form a npc to another npc
    SendItem = 1,
    // eliminate target enemy
    Eliminate = 2,
    // gather item that requested
    Collect = 3
}

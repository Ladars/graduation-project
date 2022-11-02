using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest  //���������Ϣ
{
    public enum QuestType { Gathering, Talk,Reach};
    public enum QuestStatus { Waiting,Accepted,Completed};
    public string questName;
    public QuestType questType;
    public QuestStatus questStatus;
    [TextArea(1, 6)]
    public string questDescription;

    

    [Header("Gathering Type Quest")]
    public int requireAmout;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour  //此脚本将挂载到可使任务完成的游戏对象上
{
    public string questName;
    public enum QuestType { Gathering,Talk,Reach};
    public QuestType questType;

    [Header("Gathering Type Quest")]
    public int amount;
    [Header("Talk Type Quest")]
    public bool hasTalked;
    [Header("Reach Type Quest")]
    public bool hasReached;

    public void QuestComplete() //任务完成之后调用此方法
    {
        for(int i = 0; i < Player.instance.questList.Count; i++)
        {
            if(questName == Player.instance.questList[i].questName
                && Player.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                switch (questType)
                {
                    case QuestType.Gathering:
                        if(Player.instance.itemAmout >= Player.instance.questList[i].requireAmout)
                        {
                            Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            
                            //QuestManager.instance.UpdateQuestList();
                        }
                        break;

                    case QuestType.Reach:
                        if (hasReached)
                        {
                            Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            //QuestManager.instance.UpdateQuestList();
                        }
                        break;

                    case QuestType.Talk:
                        if (hasTalked)
                        {
                            Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                           // QuestManager.instance.UpdateQuestList();
                        }
                        break;

                        
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i = 0; i < Player.instance.questList.Count; i++)
            {
                if (Player.instance.questList[i].questName == questName)
                {
                   
                     if (questType == QuestType.Reach)
                    {
                        hasReached = true;
                        QuestComplete();
                    }
                }
            }
        }
    }
  

}

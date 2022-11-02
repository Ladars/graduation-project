using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questable : MonoBehaviour//�˽ű�������ص�������ί�ɵ�NPC����
{
    public Quest quest;

    public bool isFinished;

    public Player player;

    public QuestTarget questTarget;

    public int expReward;
    public void DelegateQuest()
    {
        if(isFinished == false)
        {
            if (quest.questStatus == Quest.QuestStatus.Waiting)
            {
                Player.instance.questList.Add(quest);
                quest.questStatus = Quest.QuestStatus.Accepted;
                if (quest.questType == Quest.QuestType.Gathering)
                {
                    questTarget.QuestComplete();
                }
            }        
            else
            {
                Debug.Log(string.Format("Quest:{0} has already accepted.", quest.questName));
            }
        }
    }
  
    private void Update()
    {
        questTarget.QuestComplete();
        //gainExp();
    }
    public void gainExp()
    {
        if(quest.questStatus == Quest.QuestStatus.Completed)
        {
            player.exp = expReward;
        }
    }
}

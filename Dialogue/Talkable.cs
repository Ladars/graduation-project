using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour  //�˽ű�������ص��ɶԻ���NPC����,�ɶԻ��ű�:��������Ի����ı���Ϣ
{
    [SerializeField] private bool isEntered;
    [TextArea(1, 3)]
    public string[] lines;

    public Questable questable;
    public QuestTarget questTarget;

    [TextArea(1, 6)]
    public string[] newlines;
    [TextArea(1, 6)]
    public string[] anotherTextLine;

    public bool holdQuest;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = true;
            DiaManager.instance.revelInteraction();

            DiaManager.instance.currentQuestable = questable;
            DiaManager.instance.questTarget = questTarget;
            DiaManager.instance.talkable = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = false;
            DiaManager.instance.currentQuestable = null;
            DiaManager.instance.InteractionInfo.SetActive(false);
        }
    }
    private void Update()
    {
        if(isEntered && Input.GetKeyDown(KeyCode.E) &&DiaManager.instance.dialogueBox.activeInHierarchy ==false)
        {
            if(questable.quest.questStatus == Quest.QuestStatus.Completed)
            {
                DiaManager.instance.ShowDialogue(newlines);
            }
            else
            {
                DiaManager.instance.ShowDialogue(lines);
            }
            //DiaManager.instance.InteractionInfo.SetActive(false);
        }
        if (!isEntered)
        {
           // DiaManager.instance.InteractionInfo.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DiaManager : MonoBehaviour
{
    public GameObject dialogueBox; //�����Ի���
    public TMP_Text contentText, nameText;
    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

    private bool isScrolling;

    public static DiaManager instance; //����ģʽ
    public float textSpeed;
    public GameObject InteractionInfo;

    public Questable currentQuestable;//��ǰNPC��ӵ�е�����
    public QuestTarget questTarget;
    public Talkable talkable;

    public GameObject QuestUI;
    private PlayerSoundEffect soundEffect;

    public Animator InteractAnim;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        soundEffect = FindObjectOfType<PlayerSoundEffect>();
        contentText.text = dialogueLines[currentLine];
    }
    private void Update()
    {
        DiaFunc();
    }
    public void DiaFunc()//�Ի��ܹ���
    {
        if (dialogueBox.activeInHierarchy)
        {
            InteractionInfo.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                if (isScrolling == false)
                {
                    soundEffect.ClickSoundEvent();
                    currentLine++;
                    
                    if (currentLine < dialogueLines.Length)
                    {
                        CheckName();
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        //Debug.Log("Level select");
                        if (currentQuestable.isFinished ==false) //CheckQuestIsComplete() &&
                        {
                            //ShowDialogue(talkable.newlines);
                            currentQuestable.isFinished = true;
                        }
                        else  //���û��������񣬼���ִ��֮ǰ��ί������
                        {
                            dialogueBox.SetActive(false);
                            if (talkable.holdQuest)
                            {
                                QuestUI.SetActive(true);
                                Cursor.visible = true;
                            }
                            if (currentQuestable == null)
                            {
                                Debug.Log("There is no quest here now");
                            }
                            else
                            {
                                currentQuestable.DelegateQuest();
                                //QuestManager.instance.UpdateQuestList();
                                if (currentQuestable.isFinished ==false)//if the commission is already completed,show the new line   CheckQuestIsComplete() &&
                                {
                                    ShowDialogue(talkable.newlines);
                                    currentQuestable.isFinished =true;
                                }
                            }
                            FindObjectOfType<PlayerMovement>().interation = false;
                            if (questTarget != null)
                            {
                                for (int i = 0; i < Player.instance.questList.Count; i++)
                                {
                                    if (Player.instance.questList[i].questName == questTarget.questName)
                                    {
                                        questTarget.hasTalked = true;
                                        questTarget.QuestComplete();
                                    }
                                }                                      
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ShowDialogue(string[] _newline) //��ʾ�Ի�
    {
        dialogueLines = _newline;
        currentLine = 0;
       // CheckName();

        //contentText.text = dialogueLines[currentLine];
        StartCoroutine(ScrollingText());
        dialogueBox.SetActive(true);

        FindObjectOfType<PlayerMovement>().interation = true;
    }
    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-","");
            currentLine++;
        }
    }

    private IEnumerator ScrollingText()  //�������������ʾЧ��
    {
        isScrolling = true;
        contentText.text = "";//��ʼ�ѶԻ����ı����
        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            contentText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }
    public void revelInteraction()
    {
        InteractionInfo.SetActive(true);
        InteractAnim.SetTrigger("Interact");
    }
    public void hideInteration()
    {
        InteractionInfo.SetActive(false);
    }
    public bool CheckQuestIsComplete() //NPCί������󣬵��ô˷��������ί�����񷵻�true,��֮Ϊfalse
    {
        if(currentQuestable == null)
        {
            return false;
        }
        for(int i = 0; i < Player.instance.questList.Count; i++)
        {
            if (currentQuestable.quest.questName == Player.instance.questList[i].questName
                && Player.instance.questList[i].questStatus ==Quest.QuestStatus.Completed)
            {
                currentQuestable.quest.questStatus = Quest.QuestStatus.Completed;
                return true;
            }
        }
        return false;
    }
}

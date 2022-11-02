using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour  //该脚本负责任务信息的UI显示
{
    public static QuestManager instance;


    public GameObject[] questButton;


    public GameObject QuestUI;

    private int questNumber;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }

    #region
    //private void Start()
    //{
    //    QuestUI.SetActive(false);
    //}
    //public void UpdateQuestList()  //更新任务UI
    //{
    //    for(int i = 0; i < Player.instance.questList.Count; i++)//遍历玩家所拥有的任务
    //    {
    //        questButton[i].transform.GetChild(0).GetComponent<TMP_Text>().text = Player.instance.questList[i].questName;           
    //    }
    //}
    //public void QuestImformation()
    //{
    //    switch(questNumber)
    //    {

    //    }
    //   for(int i = 0; i < questButton.Length; i++)
    //    {
    //        questButton[0].transform.GetChild(1).GetComponent<TMP_Text>().text = Player.instance.questList[i].questDescription;
    //    }

    //        //questStatus.transform.GetChild(0).GetComponent<TMP_Text>().text = "<color=red>" + Player.instance.questList[0].questStatus + "</color>";      
    //}
    #endregion   //old code
    private void Update()
    {
        //UpdateQuestList();
        if (Input.GetKeyDown(KeyCode.Tab) )
        {
            QuestUI.SetActive(!QuestUI.activeInHierarchy);
        }
    }
}


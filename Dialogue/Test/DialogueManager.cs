using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;    //对话内容文本
    public TMP_Text name;
    public TextAsset dialogueAsset;
    
    public int dialogeIndex;
    public string[] dialogeRows; //对话文本按行分割
    public Button next;
    private void Start()
    {
        //UpdateText("my name is Tom","Tom");
        ReadText(dialogueAsset);
        showDialogRow();

    }
    public void UpdateText(string _text,string _name)
    {
        dialogueText.text = _text;
        name.text = _name;
    }
    public void ReadText(TextAsset textAsset)
    {
        dialogeRows = textAsset.text.Split('\n');
        //foreach(var row in rows)  //对文本的所以行进行遍历
        //{
        //    string[] cell = row.Split(',');
        //}
        Debug.Log("读取成功");
    }
    public void showDialogRow()
    {
       for(int i=0;i<dialogeRows.Length;i++)
        {
            string[] cells = dialogeRows[i].Split(',');//把文本按单元格分开

            UpdateText(cells[2], cells[1]);
            dialogeIndex = int.Parse(cells[3]);
            //if (int.Parse(cells[0]) == dialogeIndex && cells[4]=="#")
            //{
                
            //}
        }
    }
    public void onClickNext()  //对话框继续按钮
    {
        showDialogRow();
    }
}

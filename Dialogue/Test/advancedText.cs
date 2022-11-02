using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;


public class AdvancedTextPreprocessor : ITextPreprocessor   //文本预处理器
{
    public Dictionary<int, float> IntervalDictionary;
    
    public AdvancedTextPreprocessor()
    {
        IntervalDictionary = new Dictionary<int, float>();  //实例化
    }
    
    public string PreprocessText(string text)  //去文本中除尖括号和里面的内容
    {
        IntervalDictionary.Clear();
        string processingText = text;
        string pattern = "<.*?>";
        Match match =Regex.Match(processingText,pattern);
        while (match.Success)                       //去除文本里的富文本中的尖括号和里面的字符（数字除外）  
                                                                   // remove the curly bracket and the string  inside it except the numbers
        {
            string label = match.Value.Substring(1,match.Length-2);

            if(float.TryParse(label, out float result))
            {
                IntervalDictionary[match.Index - 1] = result;  //匹配的尖括号的前一个字符为所要停顿点
            }
            processingText = processingText.Remove(match.Index, match.Index);
            if (Regex.IsMatch(label, "^sprite=.+"))
            {               
                processingText = processingText.Insert(match.Index,"*");//如果输入的字符为图片，则先把图片删掉再用其他字符更替
            }
            
            match = Regex.Match(processingText, pattern);
        }
        // .   任意字符
        // \d 前一个字符出现十进制
        // ?  前一个字符出现出现一次或零次
        // +  前一个字符出现出现零次或多次
        // *   前一个字符重复零次或多次
        processingText = text;
        pattern = @"<(\d+)(\.\d+)?>";
        processingText = Regex.Replace(processingText, pattern, "" );
        return processingText;
    }
}
public class advancedText : TextMeshProUGUI
{
    private int _typingIndex;
    [SerializeField]private float _defaultInterval = 0.08f; //默认字符的输出间隔
    public advancedText()   //构造器
    {
        textPreprocessor = new AdvancedTextPreprocessor();
    }
    private AdvancedTextPreprocessor SelfPreprocessor => (AdvancedTextPreprocessor)textPreprocessor;//获取自己定义的类
    public void ShowTextByTyping(string content)
    {
        SetText(content);
        StartCoroutine(Typing());
    }
    
    IEnumerator Typing()
    {
        ForceMeshUpdate();
        for(int i = 0; i < m_characterCount; i++)
        {
            SetSingleCharacterAlpha(i,0);//128为半透明
        }
        _typingIndex = 0;
        while (_typingIndex< m_characterCount)
        {
            if (textInfo.characterInfo[_typingIndex].isVisible)
            {
                StartCoroutine(FadeInCharacter(_typingIndex));
            }
           
            if(SelfPreprocessor.IntervalDictionary.TryGetValue(_typingIndex,out float result))
            {
                yield return new WaitForSecondsRealtime(result);
            }
            else
            {
                yield return new WaitForSecondsRealtime(_defaultInterval);
            }
            _typingIndex++;
        }
        yield return null;
    }
   private void SetSingleCharacterAlpha(int index, byte newAlpha)
    {
        TMP_CharacterInfo charInfo = textInfo.characterInfo[index]; 
        int matIndex = charInfo.materialReferenceIndex;   //获取字符的材质索引
        int vertIndex = charInfo.vertexIndex;                       //获取字符的顶点索引

        for(int i = 0; i < 4; i++) //一个字符由四个顶点组成
        {
            textInfo.meshInfo[matIndex].colors32[vertIndex + i].a =newAlpha;
        }
        UpdateVertexData();
    }                                                                                   //通过改变字符的顶点以实现文字的逐行播放
    IEnumerator FadeInCharacter(int index,float duration =0.5f)  //渐变效果
    {
        if(duration <= 0)
        {
            SetSingleCharacterAlpha(index, 255);
        }
        else
        {
            float timer = 0;
            while(timer < duration)
            {
                timer = Mathf.Min(duration, timer + Time.unscaledDeltaTime);
                SetSingleCharacterAlpha(index, (byte)(255 * timer / duration));
                yield return null; 
            }
        }
       
    }
}


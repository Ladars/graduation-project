using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;


public class AdvancedTextPreprocessor : ITextPreprocessor   //�ı�Ԥ������
{
    public Dictionary<int, float> IntervalDictionary;
    
    public AdvancedTextPreprocessor()
    {
        IntervalDictionary = new Dictionary<int, float>();  //ʵ����
    }
    
    public string PreprocessText(string text)  //ȥ�ı��г������ź����������
    {
        IntervalDictionary.Clear();
        string processingText = text;
        string pattern = "<.*?>";
        Match match =Regex.Match(processingText,pattern);
        while (match.Success)                       //ȥ���ı���ĸ��ı��еļ����ź�������ַ������ֳ��⣩  
                                                                   // remove the curly bracket and the string  inside it except the numbers
        {
            string label = match.Value.Substring(1,match.Length-2);

            if(float.TryParse(label, out float result))
            {
                IntervalDictionary[match.Index - 1] = result;  //ƥ��ļ����ŵ�ǰһ���ַ�Ϊ��Ҫͣ�ٵ�
            }
            processingText = processingText.Remove(match.Index, match.Index);
            if (Regex.IsMatch(label, "^sprite=.+"))
            {               
                processingText = processingText.Insert(match.Index,"*");//���������ַ�ΪͼƬ�����Ȱ�ͼƬɾ�����������ַ�����
            }
            
            match = Regex.Match(processingText, pattern);
        }
        // .   �����ַ�
        // \d ǰһ���ַ�����ʮ����
        // ?  ǰһ���ַ����ֳ���һ�λ����
        // +  ǰһ���ַ����ֳ�����λ���
        // *   ǰһ���ַ��ظ���λ���
        processingText = text;
        pattern = @"<(\d+)(\.\d+)?>";
        processingText = Regex.Replace(processingText, pattern, "" );
        return processingText;
    }
}
public class advancedText : TextMeshProUGUI
{
    private int _typingIndex;
    [SerializeField]private float _defaultInterval = 0.08f; //Ĭ���ַ���������
    public advancedText()   //������
    {
        textPreprocessor = new AdvancedTextPreprocessor();
    }
    private AdvancedTextPreprocessor SelfPreprocessor => (AdvancedTextPreprocessor)textPreprocessor;//��ȡ�Լ��������
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
            SetSingleCharacterAlpha(i,0);//128Ϊ��͸��
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
        int matIndex = charInfo.materialReferenceIndex;   //��ȡ�ַ��Ĳ�������
        int vertIndex = charInfo.vertexIndex;                       //��ȡ�ַ��Ķ�������

        for(int i = 0; i < 4; i++) //һ���ַ����ĸ��������
        {
            textInfo.meshInfo[matIndex].colors32[vertIndex + i].a =newAlpha;
        }
        UpdateVertexData();
    }                                                                                   //ͨ���ı��ַ��Ķ�����ʵ�����ֵ����в���
    IEnumerator FadeInCharacter(int index,float duration =0.5f)  //����Ч��
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


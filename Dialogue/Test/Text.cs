using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] private advancedText _text;
    [Multiline]
    [SerializeField] private string content;

    private void Start()
    {
        _text.ShowTextByTyping(content);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour
{

    public string EnglishWords;
    public string ChineseWords;

    public Text text;

    private void Awake()
    {
        /*
        text = GetComponent<Text>();
        if (!text)
        {
            Debug.LogError("Text.cs未获取到Text组件");
        }
        */
        text.text = ChineseWords;       //默认中文
        EventManager.ChineseEvent.AddListener(Change2Chinese);
        EventManager.EnglishEvent.AddListener(Change2English);
    }

    void Change2English()
    {
        text.text = EnglishWords;
    }

    void Change2Chinese()
    {
        text.text = ChineseWords;
    }



    
}

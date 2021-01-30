using UnityEngine;


public class WordsData
{

    public TextAsset file;

    string[] words;                  //充当指针
    public string[] ChineseSimplified;
    public string[] English;
    int index = 0;                  //标识正在使用的句子

    public WordsData(TextAsset f)
    {
        file = f;
        ReadFormJson();
    }

    public void ReadFormJson()
    {
        JsonReader reader = new JsonReader();
        reader.Read(this);
        words = ChineseSimplified;
    }

    public string ReadNext()
    {
        index++;
        if(index >= words.Length)
        {
            return null;
        }
        return words[index];
    }

    public string Read()
    {
        return words[index];
    }

    //True为中文     Flase为英文
    public string ChangeLanguages(bool languages)           
    {
        words = languages ? ChineseSimplified : English;
        return Read();
    }
}

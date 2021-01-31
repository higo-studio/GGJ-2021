using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonReader
{
    
    public void Read(WordsData data)
    {
        
        JsonUtility.FromJsonOverwrite(data.file.text, data);
    }
}

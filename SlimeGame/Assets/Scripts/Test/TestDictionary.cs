using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDictionary : MonoBehaviour
{
    Dictionary<Vector2, int> dic = new Dictionary<Vector2, int>();

    // Start is called before the first frame update
    void Start()
    {
        dic.Add(Vector2.zero, 0);
        dic.Add(new Vector2(1, 1), 1);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dic.ContainsKey(new Vector2(1, 1)));
    }
}

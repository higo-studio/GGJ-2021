using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{

    //音效
    public AudioSource audio;

    private void Start()
    {
        audio.Play();
    }

}

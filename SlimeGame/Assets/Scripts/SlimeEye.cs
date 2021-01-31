using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEye : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject slime;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var worldClickPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = slime.transform.position+(worldClickPos - transform.position).normalized * 0.5f;
    }
}

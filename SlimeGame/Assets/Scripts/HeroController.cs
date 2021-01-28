using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 tempV2;

    public float Speed = 1;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        tempV2 = new Vector2();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // var horizon = Input.GetAxis("Horizontal");
        // var vertical = Input.GetAxis("Vertical");
        // var sourceSpd = body.velocity;
        // tempV2.Set(horizon * Speed, sourceSpd.y);
        // body.velocity = tempV2;
    }
}

using UnityEngine;

[RequireComponent(typeof(SoftBody2D), typeof(Rigidbody2D))]
public class SlimeCharacterController : MonoBehaviour
{
    private Rigidbody2D body;
    private SoftBody2D softBody;

    public float jumpVel = 5;
    public float runSpeed = 4;

    private bool _jumpState = false;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        softBody = GetComponent<SoftBody2D>();    
    }

    void Update()
    {
        var jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            _jumpState = true;
        }
    }

    void FixedUpdate()
    {
        var horitonal = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horitonal * runSpeed, 0);

        Debug.Log($"IsOnGround: {softBody.IsOnground}");
        if (_jumpState && softBody.IsOnground)
        {
            softBody.Jump(5);
            _jumpState = false;
        };
    }
}
using UnityEngine;

public class SlimeScene : MonoBehaviour
{
    public Rigidbody2D body;
    public SoftBody2D softBody;

    private bool _jumpState = false;

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
        var horitonal = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horitonal * Time.fixedDeltaTime * 100, 0);

        Debug.Log($"IsOnGround: {softBody.IsOnground}");
        if (_jumpState && softBody.IsOnground)
        {
            softBody.Jump();
            _jumpState = false;
        };
    }
}
using UnityEngine;

[RequireComponent(typeof(SoftBody2D), typeof(Rigidbody2D))]
public class SlimeCharacterController : MonoBehaviour
{
    private Rigidbody2D body;
    private SoftBody2D softBody;

    public float jumpVel = 7;
    public float runSpeed = 4;

    public GameObject bulletPrefab;
    private bool _jumpState = false;

    private Camera mainCamera;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        softBody = GetComponent<SoftBody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        var jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            _jumpState = true;
        }

        var fire = Input.GetMouseButtonDown(0);
        if (fire)
        {
            var worldClickPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(worldClickPos);
            Bb((worldClickPos - transform.position).normalized);
        }
    }

    void FixedUpdate()
    {
        var horitonal = Input.GetAxisRaw("Horizontal");
        if (softBody.IsOnground)
        {
            body.velocity = new Vector2(horitonal * runSpeed, 0);
        }

        Debug.Log($"IsOnGround: {softBody.IsOnground}");
        if (_jumpState && softBody.IsOnground)
        {
            softBody.Jump(jumpVel);
        };
        _jumpState = false;
    }

    void Bb(Vector2 initSpeed)
    {
        var obj = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var bb = obj.GetComponent<BbBullet>();
        bb._initDir = initSpeed;
    }
}
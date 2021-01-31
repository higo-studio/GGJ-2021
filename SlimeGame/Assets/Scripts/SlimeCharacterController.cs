using UnityEngine;

[RequireComponent(typeof(SoftBody2D), typeof(Rigidbody2D))]
public class SlimeCharacterController : MonoBehaviour
{
    private Rigidbody2D body;
    private SoftBody2D softBody;

    public float jumpVel = 7;
    public float runSpeed = 4;
    public Texture2D aimCursorTxt;
    public Texture2D normalCursorTxt;

    public GameObject bulletPrefab;
    public float airDumping = 0.5f;
    private bool _jumpState = false;
    private bool _velBroken = false;

    private Camera mainCamera;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        softBody = GetComponent<SoftBody2D>();
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        if (aimCursorTxt != null)
        {
            Cursor.SetCursor(aimCursorTxt, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnDisable()
    {
        if (normalCursorTxt != null)
        {
            Cursor.SetCursor(normalCursorTxt, Vector2.zero, CursorMode.Auto);
        }
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

            var delta = worldClickPos - transform.position;
            Bb(delta.normalized, delta.magnitude * 2);
        }
    }

    void FixedUpdate()
    {
        var horitonal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        // var sourVel = body.velocity;
        // sourVel.x = horitonal * runSpeed * (softBody.IsOnground ? 1 : airDumping);
        // body.velocity = sourVel;
        softBody.GravityRate = softBody.IsOnWall ? 0 : 1;
        if (softBody.IsOnWall)
        {
            var sourceVel = body.velocity;
            sourceVel.y = vertical * runSpeed;
            body.velocity = sourceVel;
        }

        if (softBody.IsOnground)
        {
            var sourceVel = body.velocity;
            sourceVel.x = horitonal * runSpeed;
            body.velocity = sourceVel;
        }
        else
        {
            var sourceVel = body.velocity;
            if (!_velBroken && sourceVel.x * horitonal < 0)
            {
                _velBroken = true;
                Debug.Log("Broken");
            }
            if (_velBroken)
            {
                sourceVel.x = horitonal * runSpeed * airDumping;
                body.velocity = sourceVel;
            }
        }


        if (_jumpState && (softBody.IsOnground || softBody.IsOnWall))
        {
            softBody.Jump(jumpVel);
            _velBroken = false;
        };
        _jumpState = false;
    }

    void Bb(Vector2 initSpeed, float speed)
    {
        var obj = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var bb = obj.GetComponent<BbBullet>();
        bb.physicsPos = transform.position;
        bb._initDir = initSpeed;
        bb.Speed = speed;
    }
}
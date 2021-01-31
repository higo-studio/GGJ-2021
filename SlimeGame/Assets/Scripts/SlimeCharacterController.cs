using UnityEngine;

[RequireComponent(typeof(SoftBody2D), typeof(Rigidbody2D))]
public class SlimeCharacterController : MonoBehaviour
{
    private Rigidbody2D body;
    private SoftBody2D softBody;

    public float minJumpVel = 7f;
    public float maxJumpVel = 14f;
    public AnimationCurve jumpWithHpCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    private float jumpVel =>
        Mathf.Lerp(minJumpVel, maxJumpVel, jumpWithHpCurve.Evaluate(slime.HP / slime.MaxHP));

    public float minSoftBodyRadius = 0.5f;
    public float maxSoftBodyRadius = 1.75f;
    public AnimationCurve softBodyRadiusWithHpCurve
        = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    private float softBodyRadius =>
        Mathf.Lerp(minSoftBodyRadius, maxSoftBodyRadius, softBodyRadiusWithHpCurve.Evaluate(slime.HP / slime.MaxHP));

    public float runSpeed = 4;
    public Texture2D aimCursorTxt;
    public Texture2D normalCursorTxt;
    private LineRenderer _lineRenderer;
    public float AimSimulateTime = 1f;

    public GameObject bulletPrefab;
    public float airDumping = 0.5f;
    public bool enableClimb = false;
    private bool _jumpState = false;
    private bool _velBroken = false;

    private Camera mainCamera;
    public AudioSource jumpAudio;
    private bool _isAiming = false;
    public float AimLineSimulationTime = 1f;
    private Slime slime;

    private Vector3[] aimLinePointArr = new Vector3[1000];
    // private 

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        softBody = GetComponent<SoftBody2D>();
        slime = GetComponent<Slime>();
        mainCamera = Camera.main;

        _lineRenderer = GameObject.Find("AimLine")?.GetComponent<LineRenderer>();
        if (!_lineRenderer)
        {
            Debug.LogWarning("Cannot find AimLine(LineRender)!");
        }
        softBody.DebugRadius = softBodyRadius;
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

        var worldClickPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var delta = worldClickPos - transform.position;

        var line = _lineRenderer;
        var canShoot = slime.HP > 1;

        var fireDown = Input.GetMouseButtonDown(0);
        if (fireDown)
        {
            line.startColor = line.endColor = canShoot ? Color.white : Color.red;
            _isAiming = true;
            line.gameObject.SetActive(true);
        }

        var fireUp = Input.GetMouseButtonUp(0);
        if (fireUp)
        {
            Debug.Log(worldClickPos);

            Bb(delta.normalized, delta.magnitude * 2);

            _isAiming = false;
            line.gameObject.SetActive(false);
        }

        if (_isAiming)
        {
            var (arr, count) = SimulateAimLine(AimSimulateTime, transform.position, Physics2D.gravity.y, delta * 2);
            line.positionCount = count;
            line.SetPositions(arr);
        }
    }

    void FixedUpdate()
    {
        Debug.Log($"wall: {softBody.IsOnWall}, ground: {softBody.IsOnground}");
        var horitonal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        // var sourVel = body.velocity;
        // sourVel.x = horitonal * runSpeed * (softBody.IsOnground ? 1 : airDumping);
        // body.velocity = sourVel;
        softBody.GravityRate = softBody.IsOnWall && enableClimb ? 0 : 1;

        if (softBody.IsOnWall && enableClimb)
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


        if (_jumpState && (softBody.IsOnground || (softBody.IsOnWall && enableClimb)))
        {
            if (softBody.IsOnWall)
            {
                var sourceVel = body.velocity;
                sourceVel.x = softBody.HitNormal.x * runSpeed * 5;
                body.velocity = sourceVel;
            }
            jumpAudio.Play();
            softBody.Jump(jumpVel);
            _velBroken = false;
        };
        _jumpState = false;

    }

    void Bb(Vector2 initSpeed, float speed)
    {
        if (slime.HP <= 1)
        {
            return;
        }

        var obj = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var bb = obj.GetComponent<BbBullet>();
        bb.physicsPos = transform.position;
        bb._initDir = initSpeed;
        bb.Speed = speed;

        slime.Hurt(1);
        softBody.DebugRadius = softBodyRadius;
    }

    public void CollectBall()
    {
        slime.Absorb(1);
        softBody.DebugRadius = softBodyRadius;
    }

    public (Vector3[], int) SimulateAimLine(float simulateTime, Vector2 start, float gravity, Vector2 initSpeed)
    {
        var dt = Time.fixedDeltaTime;
        var count = Mathf.Min(
            aimLinePointArr.Length,
            Mathf.FloorToInt(simulateTime / dt)
        );

        aimLinePointArr[0] = start;
        var pos = start;
        var velocity = initSpeed;

        for (var i = 1; i < count; i++)
        {
            velocity.y += gravity * dt;
            pos += velocity * dt;
            aimLinePointArr[i] = pos;
        }

        return (aimLinePointArr, count);
    }

    public void RefreshRadius()
    {
        softBody.DebugRadius = softBodyRadius;
    }
}
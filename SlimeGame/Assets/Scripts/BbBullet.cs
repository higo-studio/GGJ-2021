using System;
using UnityEngine;

public class BbBullet : MonoBehaviour
{
    

    public float Speed = 1;
    public float GravityRate = 1;
    public Vector2 _initDir;
    

    public float Radius = 0.4f;
    public bool isHit = false;

    public Vector2 _verticalVelocity;
    public LayerMask layerMask = 0;

    private RaycastHit2D[] _tempRaycastHitArr = new RaycastHit2D[10];

    public Vector2 physicsPos;

    TilesManager tilesManager;
    private void Awake()
    {
        tilesManager = transform.Find("Prepare").GetComponent<TilesManager>();
    }
    protected void FixedUpdate()
    {
        if (isHit) return;
        var dt = Time.fixedDeltaTime;
        var deltaPos = new Vector2();
        var gravity = Physics2D.gravity * GravityRate;
        deltaPos += Speed * _initDir * dt;

        _verticalVelocity += gravity * dt;
        deltaPos += _verticalVelocity * dt;
        var resultCount = Physics2D.CircleCastNonAlloc(transform.position, Radius, deltaPos.normalized, _tempRaycastHitArr, deltaPos.magnitude, layerMask);
        Debug.DrawRay(transform.position, deltaPos, Color.red, 1);

        for (var i = 0; i < resultCount; i++)
        {
            var result = _tempRaycastHitArr[i];
            if (result.collider.isTrigger)
                continue;
            isHit = true;
            Speed = 0;
            GravityRate = 0;
            physicsPos += (result.distance * deltaPos.normalized);
            Debug.Log(result.collider);
            Debug.Log("BbBullet: " + result.collider.name);

            // Manafer

            Destroy(gameObject, 1);


            var effects = EffectManager.ins;
            effects.GenHitEffect(result.point, result.normal);
            tilesManager.ShootOn(result.point, result.normal);

            break;
        }

        if (!isHit)
        {
            physicsPos += (deltaPos);
        }
    }

    protected void Update()
    {
        transform.position = Vector2.Lerp(transform.position, physicsPos, 0.8f);
    }
}
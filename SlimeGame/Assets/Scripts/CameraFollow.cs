using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool freezeX = false;
    public bool freezeY = false;
    public GameObject target;

    protected void LateUpdate()
    {
        var dt = Time.deltaTime;
        var source = transform.position;
        if (!freezeX)
        {
            source.x = Mathf.Lerp(source.x, target.transform.position.x, dt * 2);
        }
        if (!freezeY)
        {
            source.y = Mathf.Lerp(source.y, target.transform.position.y, dt * 2);
        }
        transform.position = source;
    }
}
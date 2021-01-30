
using UnityEngine;
using UnityEngine.U2D;
 
[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour
{
    public Color color = Color.white;
    public bool outline = true;
    private SpriteShapeRenderer spriteRenderer;
 
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteShapeRenderer>();
        UpdateOutline(outline);
    }
 
    void OnDisable()
    {
        UpdateOutline(false);
    }
 
    void Update()
    {
        UpdateOutline(outline);
    }
 
    void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
using System.Collections;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    public Material flashMaterial;
    public float duration = 0.1f;

    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    private Coroutine flashRoutine;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = defaultMaterial;
        flashRoutine = null;
    }
}

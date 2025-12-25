using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("FlashFX")] [SerializeField] private float flashDuration;

    [SerializeField] private Material hitMat;

    private Material originalMat;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    private IEnumerator flashFx()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(flashDuration);

        sr.material = originalMat;
    }

    private void RedcolorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    private void CancelRedBlink()
    {
        CancelInvoke("RedcolorBlink");
        sr.color = Color.white;
    }
}
using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("FlashFX")] [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;
    private SpriteRenderer sr;

    [Header("Aliment color")] 
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] shockColor;
    
    
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }
    
    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
            sr.color = Color.clear;
        else
        {
            sr.color = Color.white;
        }
    }

    private IEnumerator flashFx()
    {
        sr.material = hitMat;

        Color currentColor = sr.color;

        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        
        sr.material = originalMat;
    }

    private void RedcolorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }

    public void ShockFxFor(float _seconds)
    {
        InvokeRepeating("ShockColorFX", 0, .1f);
        Invoke("CancelColorChange", _seconds);
    }
    
    public void ChillFxFor(float _seconds)
    {
        InvokeRepeating("ChillColorFX", 0, .1f);
        Invoke("CancelColorChange", _seconds);
    }
    public void IgniteFxFor(float _seconds)
    {
        InvokeRepeating("IgniteColorFX", 0, .1f);
        Invoke("CancelColorChange", _seconds);
    }

    private void IgniteColorFX()
    {
        if(sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
        {
            sr.color = igniteColor[1];
        }
    }

    private void ShockColorFX()
    {
        if(sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
        {
            sr.color = shockColor[1];
        }
    }    
    private void ChillColorFX()
    {
        if(sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
        {
            sr.color = chillColor[1];
        }
    }
}
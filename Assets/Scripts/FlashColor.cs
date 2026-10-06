using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color defaultColor;

    private Tween _currTween;

    private void Start()
    {
        defaultColor = meshRenderer.material.color;
    }

    private IEnumerator FlashCoroutine()
    {
        meshRenderer.sharedMaterial.color = color;
        yield return new WaitForSeconds(duration);
        meshRenderer.sharedMaterial.color = defaultColor;
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
            
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color defaultColor;

    private Tween _currTween;

    private void Start()
    {
        if (meshRenderer != null)
        {
            defaultColor = meshRenderer.material.color;
        }
        else if (skinnedMeshRenderer != null)
        {
            defaultColor = skinnedMeshRenderer.material.color;
        }
    }

    private IEnumerator FlashCoroutine()
    {
        if (meshRenderer != null)
        {
            meshRenderer.sharedMaterial.color = color;
            yield return new WaitForSeconds(duration);
            meshRenderer.sharedMaterial.color = defaultColor;
        }
        else if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.material.color = color;
            yield return new WaitForSeconds(duration);
            skinnedMeshRenderer.material.color = defaultColor;
        }
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
            
    }
}

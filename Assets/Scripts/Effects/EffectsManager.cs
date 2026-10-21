using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public Volume processVolume;
    [SerializeField] private Vignette _vignette;
    private Color c = Color.white;

    public float duration = 1f;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        if (processVolume == null || processVolume.profile == null)
        {
            Debug.LogWarning("Volume ou perfil não atribuído.");
            return;
        }

        if (processVolume.profile.TryGet<Vignette>(out var vignette))
        {
            _vignette = vignette;
            StartCoroutine(FlashColorVignette());
        }
        else
        {
            Debug.LogWarning("O perfil não possui o efeito Vignette.");
        }
        
    }

    IEnumerator FlashColorVignette()
    {
        float time = 0;

        while (time < duration)
        {
            c = Color.Lerp(Color.white, Color.red, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        while (time < duration)
        {
            c = Color.Lerp(Color.red, Color.white, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }
            
            
    }
}

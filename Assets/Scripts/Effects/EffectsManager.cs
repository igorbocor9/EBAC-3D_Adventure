using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : MonoBehaviour
{
    public PostProcessVolume processVolume;
    [SerializeField] private Vignette _vignette;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        c.value = Color.red;

        _vignette.color.Override(c);
    }
}

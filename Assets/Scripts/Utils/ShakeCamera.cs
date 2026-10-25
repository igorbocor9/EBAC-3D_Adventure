using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using Unity.Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineCamera virtualCamera;

    public CinemachineBasicMultiChannelPerlin noise;

    public float shakeTime;

    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = .2f;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(amplitude, frequency, time);
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        noise.AmplitudeGain = amplitude;
        noise.FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            noise.AmplitudeGain = 0f;
            noise.FrequencyGain = 0f;
        }
    }
}

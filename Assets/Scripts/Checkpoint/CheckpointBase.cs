using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public int Key = 01;
    private bool isActive = false;
    private string checkpointKey = "CheckpointKey";

    private void OnTriggerEnter(Collider other)
    {
        if(!isActive && other.CompareTag("Player"))
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
        SaveCheckpoint();
    }

    [NaughtyAttributes.Button("Check Checkpoint")]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        
    }

    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckpoint()
    {
        /*if (PlayerPrefs.GetInt(checkpointKey, 0) > Key)
        {
            PlayerPrefs.SetInt(checkpointKey, Key);
        }*/

        CheckpointManager.Instance.SaveCheckpoint(Key);

        isActive = true;
    }
}

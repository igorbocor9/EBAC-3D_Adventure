using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int key)
    {
        if (key > lastCheckpointKey)
        {
            lastCheckpointKey = key;
        }
    }

    public Vector3 GetLastCheckpointPosition()
    {
        var checkpoint = checkpoints.Find(c => c.Key == lastCheckpointKey);
        return checkpoint.transform.position;
    }
}

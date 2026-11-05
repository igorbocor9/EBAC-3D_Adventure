using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothChange : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;

    public Texture2D texture;
    public string shaderIdName = "_EmissionMap";

    [NaughtyAttributes.Button("Change Texture")]
    private void ChangeTexture()
    {
        mesh.materials[0].SetTexture(shaderIdName, texture);
    }
}

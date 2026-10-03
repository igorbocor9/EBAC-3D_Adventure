using UnityEngine;

public class FlashColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    [NaughtyAttributes.Button("Flash Color")]
    private void Flash()
    {
        Material mat = meshRenderer.material;

        Debug.Log("Shader: " + mat.shader.name);

        if (mat.HasProperty("_EmissionColor"))
        {
            Debug.Log("Found _EmissionColor");

            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.red * 10f);
        }
        else
        {
            Debug.LogError("THIS MATERIAL DOES NOT HAVE _EmissionColor");
        }
    }
}

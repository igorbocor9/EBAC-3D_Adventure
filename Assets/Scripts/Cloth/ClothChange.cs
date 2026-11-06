using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cloth;

namespace Cloth
{
    public class ClothChange : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;

        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = mesh.materials[0].GetTexture(shaderIdName) as Texture2D;
        }

        [NaughtyAttributes.Button("Change Texture")]
        private void ChangeTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            mesh.materials[0].SetTexture(shaderIdName, setup.text);
        }

        public void ResetTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, _defaultTexture);
        }
    }
}

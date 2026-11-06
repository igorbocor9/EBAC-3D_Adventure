using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;

namespace Cloth
{

    public enum ClothType
    {
        SPEED,
        STRONG
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetups.Find(x => x.clothType == clothType);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D text;
    }
}

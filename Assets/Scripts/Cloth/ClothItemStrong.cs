using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cloth
{
    public class ClothItemStrong : ClothItemBase
    {
        public float targetDefense = .5f;
        public override void Collect()
        {
            base.Collect();
            Player.Instance.healthBase.ChangeDamage(targetDefense, duration);
        }
    }
}

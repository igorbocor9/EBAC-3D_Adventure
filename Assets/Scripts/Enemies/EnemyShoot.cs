using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();
            if (gunBase != null)
            {
                gunBase.StartShoot();
            }
        }
    }
}

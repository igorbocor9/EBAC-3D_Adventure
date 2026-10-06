using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDamageable
{
    void Damage(float damage);

    void Damage(float damage, Vector3 dir);
}

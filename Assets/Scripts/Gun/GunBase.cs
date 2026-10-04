using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class GunBase : MonoBehaviour
{
    public ProjectileBase PrefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = 0.3f;
    public float speed = 50f;

    public bool changingGun = false;

    private Coroutine _currentCoroutine;

    protected virtual IEnumerator ShootCoroutine()
    {
        if(changingGun) yield break;

        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(PrefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
    }
}

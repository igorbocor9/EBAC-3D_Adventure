using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunShootLimit : GunBase
{
    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentshoots;
    private bool _recharging = false;

    protected override IEnumerator ShootCoroutine()
    {
        if(_recharging) yield break;

        while (true)
        {
            if(_currentshoots < maxShoot)
            {
                Shoot();
                _currentshoots++;
                CheckRecharge();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckRecharge()
    {
        if(_currentshoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            Debug.Log("Recharging" + time);
            yield return new WaitForEndOfFrame();
        }
        _currentshoots = 0;
        _recharging = false;
    }
}

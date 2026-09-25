using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<UIGunUpdater> uiGunUpdaters;
    public GunBase gunBase;
    public List<GunBase> Guns;
    public Transform gunPosition;

    private GunBase _currentGun;
    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
        inputs.Gameplay.Gun1.performed += cts => ChangeGun(0);
        inputs.Gameplay.Gun2.performed += cts => ChangeGun(1);
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel Shoot");
    }

    public void ChangeGun(int gun)
    {
        //gunBase = Guns[gun];
        if (gunBase != Guns[gun])
        {
            Destroy(_currentGun);
            gunBase = Guns[gun];
            CreateGun();
        }
    }
}

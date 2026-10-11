using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public List<UIFillUpdater> uiFillUpdater;

    public void Awake()
    {
        Init();
    }

    protected void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 3f);
        }

        (OnKill)?.Invoke(this);
    }

    [NaughtyAttributes.Button("Damage")]
    public void DamageButton()
    {
        Damage(1f);
    }

    public void Damage(float damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uiFillUpdater != null)
        {
            uiFillUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }
}

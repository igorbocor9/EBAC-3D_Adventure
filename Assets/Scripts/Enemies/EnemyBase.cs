using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public FlashColor flashColor;
        public ParticleSystem particleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;
        [SerializeField] public float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;

        private Player _player;

        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        public bool startWithBornAnimation = true;

        private void Awake()
        {
            Init();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithBornAnimation)
                BornAnimation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if(collider != null) collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            if (flashColor != null)
            {
                flashColor.Flash();
            }

            if (particleSystem != null)
            {
                particleSystem.Play();
            }


            _currentLife -= f;

            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f).SetEase(Ease.OutBack);
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Debug.Log($"Player took damage!");
            if (player != null)
            {
                player.Damage(1f);
            }
        }


        public virtual void Update()
        {
            if (lookAtPlayer && _player != null)
            {
                var playerPosition = _player.transform.position;
                playerPosition.y = transform.position.y; // Keep the enemy's y position unchanged
                transform.LookAt(playerPosition);
            }
        }

    }
}

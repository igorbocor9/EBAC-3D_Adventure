using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EBAC.StateMachine;
using DG.Tweening;
using System;

namespace Boss
{

    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;

        public float speed = 5f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        private StateMachine<BossAction> stateMachine;

        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        private void OnBossKill(HealthBase healthBase)
        {
            SwitchState(BossAction.DEATH);
        }

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        private IEnumerator AttackCoroutine(Action endCallback = null)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                Debug.Log("Attack");
                attacks++;
                transform.DOScale(1.5f, .2f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endCallback?.Invoke();
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        private IEnumerator GoToPointCoroutine(Transform point, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, point.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button("SwitchInit")]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }
        [NaughtyAttributes.Button("SwitchWalk")]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }
        [NaughtyAttributes.Button("SwitchAttack")]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }

        #endregion

        #region StateMachine

        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }

        #endregion
    }
}

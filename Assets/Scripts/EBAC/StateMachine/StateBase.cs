using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EBAC.StateMachine
{
    public class StateBase
    {
        protected Player playerbase = Object.FindObjectOfType(typeof(Player)) as Player;
        public virtual void OnStateEnter()
        {
            Debug.Log("OnStateEnter");
        }
        public virtual void OnStateStay()
        {
            Debug.Log("OnStateStay");
        }
        public virtual void OnStateExit()
        {
            Debug.Log("OnStateExit");
        }
    }
}

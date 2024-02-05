using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace anogame
{
    public abstract class StateMachineBase<T> : MonoBehaviour where T : StateMachineBase<T>
    {
        protected StateBase<T> stateCurrent;

        [HideInInspector] public UnityEvent OnBegin = new UnityEvent();
        [HideInInspector] public UnityEvent OnEnd = new UnityEvent();

        public virtual void OnUpdatePrev() { }
        public virtual void OnUpdateAfter() { }

        public virtual void OnFixedUpdatePrev() { }
        public virtual void OnFixedUpdateAfter() { }

        public void ChangeState(StateBase<T> state)
        {
            if (stateCurrent != null)
            {
                stateCurrent.OnExitState();
            }
            stateCurrent = state;
            stateCurrent.OnEnterState();
        }
        private void Update()
        {
            OnUpdatePrev();
            if (stateCurrent != null)
            {
                stateCurrent.OnUpdateState();
            }
            OnUpdateAfter();
        }

        private void FixedUpdate()
        {
            OnFixedUpdatePrev();
            if (stateCurrent != null)
            {
                stateCurrent.OnFixedUpdateState();
            }
            OnFixedUpdateAfter();
        }
    }
}

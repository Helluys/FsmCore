using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fsm {
    [Serializable]
    public partial class FsmTransition : IEnumerable<FsmCondition> {

        public UnityEvent OnTransition = new UnityEvent();

        private List<FsmCondition> conditions = new List<FsmCondition>();

        public void AddCondition (FsmCondition condition) {
            conditions.Add(condition);
        }

        public void RemoveCondition(FsmCondition condition) {
            conditions.Remove(condition);
        }

        public void RemoveParameter (FsmParameter parameter) {
            conditions.RemoveAll(c => c.Contains(parameter));
        }

        public bool Evaluate () {
            bool result = true;
            foreach (FsmCondition condition in conditions) {
                result &= condition.Evaluate();
            }

            if (result) {
                OnTransition.Invoke();
            }

            return result;
        }

        public IEnumerator<FsmCondition> GetEnumerator () {
            return conditions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return conditions.GetEnumerator();
        }
    }
}

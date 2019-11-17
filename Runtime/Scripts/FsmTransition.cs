using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Helluys.FsmCore {
    [Serializable]
    public class FsmTransition : IEnumerable<FsmCondition> {

        public UnityEvent OnTransition = new UnityEvent();

        [SerializeField] private List<FsmCondition> conditions = new List<FsmCondition>();

        public void AddCondition (FsmCondition condition) {
            conditions.Add(condition);
        }

        public void RemoveCondition(FsmCondition condition) {
            conditions.Remove(condition);
        }

        public void RemoveParameter (FsmParameter parameter) {
            conditions.RemoveAll(c => c.Contains(parameter.name));
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

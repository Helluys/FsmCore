using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Helluys.FsmCore {
	public partial class FiniteStateMachine : ISerializationCallbackReceiver {
		[SerializeField] private List<FsmParameter> serializedParameters = new List<FsmParameter>();
		[SerializeField] private List<StateInstance> serializedStateInstances = new List<StateInstance>();

		public void OnBeforeSerialize() {
			serializedStateInstances.Clear();
			serializedParameters.Clear();

			serializedStateInstances.AddRange(stateInstances);
			serializedParameters.AddRange(parameters);
		}

		public void OnAfterDeserialize() {
			_stateInstances.Clear();
			_parameters.Clear();

			_parameters.AddRange(serializedParameters);
			_stateInstances.AddRange(serializedStateInstances);

			LinkParameters();
		}

		private void LinkParameters() {
			foreach(StateInstance si in stateInstances) {
				foreach(TransitionInstance transition in si.transitions) {
					foreach(FsmCondition condition in transition.transition) {
						foreach(FsmParameter parameter in parameters) {
							if(condition.Contains(parameter.name)) {
								condition.ReplaceParameter(parameter);
							}
						}
					}
				}
			}
		}

		private static string IncrementName(string name) {
			Regex re = new Regex(@"([^\d]*)(\d*)$");
			Match result = re.Match(name);

			string alphaPart = result.Groups[1].Value;
			string numberPart = result.Groups[2].Value;
			int number = (numberPart.Length == 0) ? 0 : int.Parse(numberPart);

			return alphaPart + (number + 1);
		}
	}
}

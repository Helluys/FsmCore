using System;
using UnityEditor;
using UnityEngine;

namespace Helluys.FsmCore {
	[Serializable]
	public class FsmCondition : ISerializationCallbackReceiver {

		public enum Type {
			TRIGGER,
			EQUALS,
			GREATER_THAN,
			SMALLER_THAN
		}

		public string parameterName {
			get {
				return _parameterName;
			}
		}

		public Type type {
			get {
				return _type;
			}

			private set {
				_type = value;
			}
		}

		public bool boolValue {
			get {
				return _boolValue;
			}

			set {
				_boolValue = value;
			}
		}

		public int intValue {
			get {
				return _intValue;
			}

			set {
				_intValue = value;
			}
		}

		public float floatValue {
			get {
				return _floatValue;
			}

			set {
				_floatValue = value;
			}
		}

		[SerializeField] private Type _type;
		private FsmParameter _parameter;
		[SerializeField] private string _parameterName;
		[SerializeField] private bool _boolValue;
		[SerializeField] private int _intValue;
		[SerializeField] private float _floatValue;

		public static FsmCondition NewTrigger(FsmParameter parameter) {
			return new FsmCondition(Type.TRIGGER, parameter, false);
		}

		public static FsmCondition NewEquals(FsmParameter parameter, bool value) {
			return new FsmCondition(Type.EQUALS, parameter, value);
		}

		public static FsmCondition NewEquals(FsmParameter parameter, int value) {
			return new FsmCondition(Type.EQUALS, parameter, value);
		}

		public static FsmCondition NewEquals(FsmParameter parameter, float value) {
			return new FsmCondition(Type.EQUALS, parameter, value);
		}

		public static FsmCondition NewGreaterThan(FsmParameter parameter, int value) {
			return new FsmCondition(Type.GREATER_THAN, parameter, value);
		}

		public static FsmCondition NewGreaterThan(FsmParameter parameter, float value) {
			return new FsmCondition(Type.GREATER_THAN, parameter, value);
		}

		public static FsmCondition NewSmallerThan(FsmParameter parameter, int value) {
			return new FsmCondition(Type.SMALLER_THAN, parameter, value);
		}

		public static FsmCondition NewSmallerThan(FsmParameter parameter, float value) {
			return new FsmCondition(Type.SMALLER_THAN, parameter, value);
		}

		private FsmCondition(Type type, FsmParameter parameter, bool value) {
			_type = type;
			_parameter = parameter;
			_boolValue = value;
			_intValue = 0;
			_floatValue = 0f;
		}

		private FsmCondition(Type type, FsmParameter parameter, int value) {
			_type = type;
			_parameter = parameter;
			_boolValue = false;
			_intValue = value;
			_floatValue = value;
		}

		private FsmCondition(Type type, FsmParameter parameter, float value) {
			_type = type;
			_parameter = parameter;
			_boolValue = false;
			_intValue = (int) value;
			_floatValue = value;
		}

		public bool Contains(string parameterName) {
			return _parameterName == parameterName;
		}

		public void ReplaceParameter(FsmParameter parameter) {
			_parameterName = parameter.name;
			_parameter = parameter;
		}

		public bool Evaluate() {
			switch(_type) {
				case Type.TRIGGER:
					return _parameter.Trigger();
				case Type.EQUALS:
					switch(_parameter.type) {
						case FsmParameter.Type.BOOLEAN:
							return _parameter.Equals(_boolValue);
						case FsmParameter.Type.INTEGER:
							return _parameter.Equals(_intValue);
						case FsmParameter.Type.FLOAT:
							return _parameter.Equals(_floatValue);
						default:
							throw new NotSupportedException("Unknown parameter type " + _parameter.type);
					}
				case Type.GREATER_THAN:
					switch(_parameter.type) {
						case FsmParameter.Type.BOOLEAN:
							throw new NotSupportedException("Cannot compare a boolean parameter");
						case FsmParameter.Type.INTEGER:
							return _parameter.GreaterThan(_intValue);
						case FsmParameter.Type.FLOAT:
							return _parameter.GreaterThan(_floatValue);
						default:
							throw new NotSupportedException("Unknown parameter type " + _parameter.type);
					}
				case Type.SMALLER_THAN:
					switch(_parameter.type) {
						case FsmParameter.Type.BOOLEAN:
							throw new NotSupportedException("Cannot compare a boolean parameter");
						case FsmParameter.Type.INTEGER:
							return _parameter.SmallerThan(_intValue);
						case FsmParameter.Type.FLOAT:
							return _parameter.SmallerThan(_floatValue);
						default:
							throw new NotSupportedException("Unknown parameter type " + _parameter.type);
					}
				default:
					throw new InvalidOperationException("Unkown condition type " + _type);
			}
		}

		public void OnBeforeSerialize() {
			_parameterName = _parameter?.name;
		}

		public void OnAfterDeserialize() {
			// Parameter will be filled by FiniteStateMachine
			_parameter = null;
		}
	}
}

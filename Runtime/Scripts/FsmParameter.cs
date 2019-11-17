using System;
using UnityEngine;

namespace Helluys.FsmCore {
	[Serializable]
	public class FsmParameter {
		public enum Type {
			BOOLEAN,
			INTEGER,
			FLOAT
		}

		public static FsmParameter NewBoolean(string name, bool value = false) {
			FsmParameter p = new FsmParameter {
				name = name,
				type = Type.BOOLEAN,
				boolValue = value
			};
			return p;
		}

		public static FsmParameter NewInteger(string name, int value = 0) {
			FsmParameter p = new FsmParameter {
				name = name,
				type = Type.INTEGER,
				intValue = value
			};
			return p;
		}

		public static FsmParameter NewFloat(string name, float value = 0f) {
			FsmParameter p = new FsmParameter {
				name = name,
				type = Type.FLOAT,
				floatValue = value
			};
			return p;
		}

		public string name {
			get {
				return _name;
			}

			set {
				_name = value;
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
				if(type != Type.BOOLEAN)
					throw new NotSupportedException("Cannot write boolean to a parameter of type " + _type);

				_boolValue = value;
			}
		}

		public int intValue {
			get {
				return _intValue;
			}

			set {
				if(type != Type.INTEGER)
					throw new NotSupportedException("Cannot write boolean to a parameter of type " + _type);

				_intValue = value;
			}
		}

		public float floatValue {
			get {
				return _floatValue;
			}

			set {
				if(type != Type.FLOAT)
					throw new NotSupportedException("Cannot write boolean to a parameter of type " + _type);

				_floatValue = value;
			}
		}

		[SerializeField] private string _name;
		[SerializeField] private Type _type = Type.BOOLEAN;
		[SerializeField] private bool _boolValue;
		[SerializeField] private int _intValue;
		[SerializeField] private float _floatValue;

		public bool Trigger() {
			if(_type != Type.BOOLEAN) {
				throw new NotSupportedException("Cannot Trigger a parameter of type " + _type);
			}

			bool result = boolValue;
			boolValue = false;

			return result;
		}

		public bool Equals(bool value) {
			switch(_type) {
				case Type.BOOLEAN:
					return boolValue == value;
				case Type.INTEGER:
				case Type.FLOAT:
					throw new NotSupportedException("Cannot compare a parameter of type " + _type + " with a boolean constant");
				default:
					throw new NotSupportedException("Unknown parameter type " + _type);
			}
		}

		public bool Equals(int value) {
			switch(_type) {
				case Type.INTEGER:
					return intValue == value;
				case Type.BOOLEAN:
				case Type.FLOAT:
					throw new NotSupportedException("Cannot compare a parameter of type " + _type + " with an integer constant");
				default:
					throw new NotSupportedException("Unknown parameter type " + _type);
			}
		}

		public bool Equals(float value) {
			switch(_type) {
				case Type.FLOAT:
					return Mathf.Abs(floatValue - value) < float.Epsilon;
				case Type.BOOLEAN:
				case Type.INTEGER:
					throw new NotSupportedException("Cannot compare a parameter of type " + _type + " with a float constant");
				default:
					throw new NotSupportedException("Unknown parameter type " + _type);
			}
		}

		public bool GreaterThan(int value) {
			switch(_type) {
				case Type.INTEGER:
					return _intValue > value;
				case Type.BOOLEAN:
				case Type.FLOAT:
					throw new NotSupportedException("Cannot compare a parameter of type " + _type + " with an integer constant");
				default:
					throw new NotSupportedException("Unknown parameter type " + _type);
			}
		}

		public bool GreaterThan(float value) {
			switch(_type) {
				case Type.FLOAT:
					return _floatValue > value;
				case Type.INTEGER:
				case Type.BOOLEAN:
					throw new NotSupportedException("Cannot compare a parameter of type " + _type + " with a float constant");
				default:
					throw new NotSupportedException("Unknown parameter type " + _type);
			}
		}

		public bool SmallerThan(int value) {
			switch(_type) {
				case Type.INTEGER:
					return _intValue < value;
				case Type.BOOLEAN:
				case Type.FLOAT:
					throw new NotSupportedException("Cannot compare a parameter of type " + _type + " with an integer constant");
				default:
					throw new NotSupportedException("Unknown parameter type " + _type);
			}
		}

		public bool SmallerThan(float value) {
			switch(_type) {
				case Type.FLOAT:
					return _floatValue < value;
				case Type.INTEGER:
				case Type.BOOLEAN:
					throw new NotSupportedException("Cannot compare a parameter of type " + _type + " with a float constant");
				default:
					throw new NotSupportedException("Unknown parameter type " + _type);
			}
		}
	}
}
